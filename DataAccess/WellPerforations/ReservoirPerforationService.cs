using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.DataAccess.Common;
using static WellClass.Well;

namespace Web_Prom.Core.Blazor.DataAccess.WellPerforations
{
    public static class ReservoirPerforationService
    {
        // Внутри всё авторства Сергея
        public static IEnumerable<int> ReservoirsByInterval(IEnumerable<VskrutCl> vskrut, float top, float baseHeight)
        {
            if (vskrut is null || !vskrut.Any())
            {
                var ir = new int[1];
                ir[0] = -999;
                return ir;
            }

            if (top < 0f)
                top = baseHeight;
            if (baseHeight < 0f)
                baseHeight = top;

            // если интервал не задан, выход
            if (top < 0f)
                return null;

            // отсортировать массив вскрытия по глубинам
            vskrut = vskrut.OrderBy(t => t.Top).ToList();
            int source = -999;
            VskrutCl[] activVstkut = null;
            // сначала ищем базовые исночники информации
            foreach (VskrutCl i in vskrut)
            {
                if (i is object && i.Source <= 0 | i.Source == DataSource.Base)
                {
                    source = DataSource.Base;
                    if (i.Source <= 0)
                        i.Source = DataSource.Base;
                    if (activVstkut is null)
                        activVstkut = new VskrutCl[1];
                    else
                        Array.Resize(ref activVstkut, activVstkut.Length + 1);
                    activVstkut[activVstkut.Length - 1] = i;
                }
            }

            if (source < 0)
            {
                foreach (VskrutCl i in vskrut)
                {
                    if (i is object && i.Source == DataSource.Geomodl)
                    {
                        source = DataSource.Geomodl;
                        if (activVstkut is null)
                            activVstkut = new VskrutCl[1];
                        else
                            Array.Resize(ref activVstkut, activVstkut.Length + 1);
                        activVstkut[activVstkut.Length - 1] = i;
                    }
                }
            }

            if (source < 0)
            {
                foreach (VskrutCl i in vskrut)
                {
                    if (i is object && i.Source == DataSource.Finder)
                    {
                        source = DataSource.Finder;
                        if (activVstkut is null)
                            activVstkut = new VskrutCl[1];
                        else
                            Array.Resize(ref activVstkut, activVstkut.Length + 1);
                        activVstkut[activVstkut.Length - 1] = i;
                    }
                }
            }

            // создадим массив в который пропишем глубины
            var mH = new float[activVstkut.Length * 2 + 1]; // массив глубин
            var mUp = new int[mH.Length, 11]; // массив паластов выше отметки
            var mDown = new int[mH.Length, 11]; // массив пластов ниже отметки

            // первым проходом вставим кровли, все
            foreach (VskrutCl i in activVstkut)
            {
                if (i is object && i.Source == source)
                {
                    // ищем верха
                    for (int x = 0, loopTo = mH.Length - 1; x <= loopTo; x++)
                    {
                        if (i.Top == mH[x] | mH[x] == 0f)
                        {
                            // если найдено совпадение или пустой элемент то добавить кровлю пласта
                            if (mH[x] == 0f)
                                mH[x] = i.Top;
                            for (int y = 0; y <= 10; y++)
                            {
                                if (mDown[x, y] == 0)
                                {
                                    mDown[x, y] = i.Plast;
                                    break;
                                }
                            }

                            break;
                        }

                        if (i.Top < mH[x] & x == 0 || x > 0 && i.Top < mH[x] & i.Top > mH[x - 1])
                        {
                            // если находимся между точками или выше первой
                            // раздвигаем массив
                            Array.Copy(mH, x, mH, x + 1, mH.Length - 1 - x);
                            Array.Copy(mDown, x, mDown, x + 1, mH.Length - 1 - x);
                            // массив MUp должен быть пустым, поэтому его не трогаем
                            // очищать последний элемент необходимости нет да и нельзя
                            // запишем полученные значения кровли
                            mH[x] = i.Top;
                            mDown[x, 0] = i.Plast;
                            // данную отметку считаем обработанной и выходим из цикла
                            break;
                        }
                    }
                }
            }
            // теперь обработаем знаемые отметки подошвы пластов
            foreach (VskrutCl i in activVstkut)
            {
                // нас интересуют только не отметки, 
                // где(прописаны) подошвы пласта
                if (i is object && i.Baze > 0f && i.Source == source)
                {
                    for (int x = 0, loopTo1 = mH.Length - 1; x <= loopTo1; x++)
                    {
                        if (i.Baze == mH[x] | mH[x] == 0f)
                        {
                            if (mH[x] == 0f)
                                mH[x] = i.Baze;
                            for (int y = 0; y <= 10; y++)
                            {
                                if (mUp[x, y] == 0)
                                {
                                    mUp[x, y] = i.Plast;
                                    break;
                                }
                            }

                            break;
                        }

                        if (i.Baze < mH[x] & x == 0 || x > 0 && i.Baze < mH[x] & i.Baze > mH[x - 1])
                        {
                            // если находимся между точками или выше первой
                            // раздвигаем массив
                            Array.Copy(mH, x, mH, x + 1, mH.Length - 1 - x);
                            for (int x1 = mH.Length - 2, loopTo2 = x; x1 >= loopTo2; x1 -= 1)
                            {
                                for (int y1 = 0; y1 <= 10; y1++)
                                {
                                    mDown[x1 + 1, y1] = mDown[x1, y1];
                                    mUp[x1 + 1, y1] = mUp[x1, y1];
                                }
                            }

                            for (int y1 = 0; y1 <= 10; y1++)
                            {
                                mDown[x, y1] = 0;
                                mUp[x, y1] = 0;
                            }
                            // Array.Copy(MDown, x, MDown, x + 1, MH.Length - 1 - x)
                            // Array.Copy(MUp, x, MUp, x + 1, MH.Length - 1 - x)
                            // массив MUp должен быть пустым, поэтому его не трогаем
                            // очищать последний элемент необходимости нет да и нельзя
                            // запишем полученные значения кровли
                            mH[x] = i.Baze;
                            mUp[x, 0] = i.Plast;
                            // данную отметку считаем обработанной и выходим из цикла
                            break;
                        }
                    }
                }
            }
            // отсортировать платы
            float[] tempArray = (float[])mH.Clone();


            // СЛЕДУЮЩИМ ЦИКЛОМ НАДО ОБРАБОТАТ ВСТАВКУ up ЭЛЕментов
            for (int x = 0, loopTo3 = mH.Length - 1; x <= loopTo3; x++)
            {
                if (mH[x] <= 0f)
                    break;
                if (mH[x] > 0f && mUp[x, 0] > 0)
                {
                    for (int y = 0; y <= 10; y++)
                    {
                        if (mUp[x, y] > 0)
                        {
                            // код копируемого пласта, группы
                            int seachPL = mUp[x, y];
                            int topI = -999;

                            // поиск верха этого пласта
                            for (int sX = 0, loopTo4 = x; sX <= loopTo4; sX++)
                            {
                                for (int sY = 0; sY <= 10; sY++)
                                {
                                    if (mDown[sX, sY] > 0)
                                    {
                                        if (seachPL == mDown[sX, sY])
                                        {
                                            topI = sX;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                // если верх был найден, тогда прописываем этот пласт всему инtервалу
                                if (topI >= 0)
                                {
                                    for (int n = topI + 1, loopTo5 = x - 1; n <= loopTo5; n++)
                                    {
                                        for (int nX = 0; nX <= 10; nX++)
                                        {
                                            if (mUp[n, nX] == 0)
                                            {
                                                mUp[n, nX] = seachPL;
                                                break;
                                            }
                                        }

                                        for (int nX = 0; nX <= 10; nX++)
                                        {
                                            if (mDown[n, nX] == 0)
                                            {
                                                mDown[n, nX] = seachPL;
                                                break;
                                            }
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            // вот теперь уже будем искать вхождения интервала
            var topIndex = default(int);
            var baseIndex = default(int);
            for (int x = 0, loopTo6 = mH.Length - 1; x <= loopTo6; x++)
            {
                if (top < mH[x])
                {
                    topIndex = x - 1;
                    break;
                }
                else if (top == mH[x])
                {
                    topIndex = x;
                    break;
                }
                else if (mH[x] == 0f)
                {
                    topIndex = x - 1;
                    break;
                }
            }

            for (int x = 0, loopTo7 = mH.Length - 1; x <= loopTo7; x++)
            {
                if (baseHeight <= mH[x])
                {
                    baseIndex = x;
                    break;
                }
                else if (mH[x] == 0f)
                {
                    baseIndex = x;
                    break;
                }
            }

            int[] plasts = null;
            // выбрать пласты
            // добавим в записи о крайних точках интервала

            // верх интервала
            if (topIndex >= 0)
            {
                for (int x = 0; x <= 10; x++)
                {
                    if (mDown[topIndex, x] > 0)
                    {
                        if (plasts is null)
                        {
                            plasts = new int[1];
                        }
                        else
                        {
                            Array.Resize(ref plasts, plasts.Length + 1);
                        }

                        plasts[plasts.Length - 1] = mDown[topIndex, x];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int x = 0; x <= 10; x++)
                {
                    if (mUp[0, x] > 0)
                    {
                        if (plasts is null)
                        {
                            plasts = new int[1];
                        }
                        else
                        {
                            Array.Resize(ref plasts, plasts.Length + 1);
                        }

                        plasts[plasts.Length - 1] = mUp[0, x];
                    }
                    else
                    {
                        break;
                    }
                }
            }


            // низ интервала
            if (baseIndex > 0)
            {
                for (int x = 0; x <= 10; x++)
                {
                    if (mUp[baseIndex, x] > 0)
                    {
                        if (plasts is null)
                        {
                            plasts = new int[1];
                        }
                        else
                        {
                            Array.Resize(ref plasts, plasts.Length + 1);
                        }

                        plasts[plasts.Length - 1] = mUp[baseIndex, x];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int x = 0; x <= 10; x++)
                {
                    if (mUp[0, x] > 0)
                    {
                        if (plasts is null)
                        {
                            plasts = new int[1];
                        }
                        else
                        {
                            Array.Resize(ref plasts, plasts.Length + 1);
                        }

                        plasts[plasts.Length - 1] = mUp[0, x];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // выборка всех пластов в интервале
            if (topIndex <= baseIndex)
            {
                for (int x = topIndex + 1, loopTo8 = baseIndex - 1; x <= loopTo8; x++)
                {
                    for (int y = 0; y <= 10; y++)
                    {
                        if (mUp[x, y] > 0)
                        {
                            if (plasts is null)
                            {
                                plasts = new int[1];
                            }
                            else
                            {
                                Array.Resize(ref plasts, plasts.Length + 1);
                            }

                            plasts[plasts.Length - 1] = mUp[x, y];
                        }

                        if (mDown[x, y] > 0)
                        {
                            if (plasts is null)
                            {
                                plasts = new int[1];
                            }
                            else
                            {
                                Array.Resize(ref plasts, plasts.Length + 1);
                            }

                            plasts[plasts.Length - 1] = mDown[x, y];
                        }
                    }
                }
            }

            if (plasts is null)
            {
                plasts = new int[1];
                plasts[0] = -999;
            }

            Array.Sort(plasts);
            int[] outPlasts = null;
            if (plasts is object)
            {
                outPlasts = new int[1];
                outPlasts[0] = plasts[0];
                for (int x = 1, loopTo9 = plasts.Length - 1; x <= loopTo9; x++)
                {
                    if (plasts[x] != outPlasts[outPlasts.Length - 1])
                    {
                        Array.Resize(ref outPlasts, outPlasts.Length + 1);
                        outPlasts[outPlasts.Length - 1] = plasts[x];
                    }
                }
            }

            return outPlasts;
        }
    }
}

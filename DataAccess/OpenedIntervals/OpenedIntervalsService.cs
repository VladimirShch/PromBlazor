using System;
using static WellClass.Well;

namespace Web_Prom.Core.Blazor.DataAccess.OpenedIntervals
{
    public class OpenedIntervalsService
    {
        // Начинка Сергеева полностью
        public static PerfCs[] GetOpenedIntervals(PerfCs[] perforations, ConstrCs construction, float zaboy, DateTime onDate)
        {
            // если дата неизвестна, то принять текущюю дату
            if (onDate == DateTime.Parse("1900-01-01"))
                onDate = DateTime.Now.Date;
            // определим верхнюю отметку цементного моста и интерфал открытого ствола
            PerfCs[] openStv = null;
            InclinCs topZab = null;
            var pakerTop = default(float);
            if (construction is object)
            {
                openStv = construction.OpenedStvol(onDate, zaboy);
                topZab = construction.ZaboyIsk(onDate);
                var lPak = construction.PakerTop(onDate);
                if (lPak is object)
                    pakerTop = lPak.MD;
                if (pakerTop > 0f && construction.Filtr is object && construction.Filtr.Enable(onDate))
                {
                    foreach (ConstrCs.EqCs trub in construction.Filtr.Sections(onDate))
                    {
                        if (trub is object)
                        {
                            if (trub.Where == 1118201/*CODES.Construction.НКТ1*/ | trub.Where == 1118201/*CODES.Construction.НКТ1*/ | trub.Where <= 0)
                            {
                                if (trub.Top > 0f & trub.Top < pakerTop)
                                {
                                    pakerTop = 0f;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (topZab is object && topZab.MD > 0f)
            {
                if (zaboy > 0f)
                    zaboy = Math.Min(topZab.MD, zaboy);
                else
                    zaboy = topZab.MD;
            }

            var pNow = PerforationUnited(perforations, zaboy, pakerTop, onDate);

            // оказалось что скважины могут стрелять и спускать фильтра

            if (pNow is null)
            {
                return PerforationUnited(openStv, zaboy, pakerTop, onDate);
            }
            else
            {
                if (openStv is object)
                {
                    int n0 = pNow.Length;
                    Array.Resize(ref pNow, n0 + openStv.Length);
                    for (int i = 0, loopTo = openStv.Length - 1; i <= loopTo; i++)
                        pNow[n0 + i] = openStv[i];
                }

                return PerforationUnited(pNow, zaboy, pakerTop, onDate);
            }


            // 'интервалы перворации c учетом искуственного забоя
            // Dim UPerf1() As Well.PerfCs = PerforationUnited(Perf, Zaboy, OnDate)

            // Return UPerf1
        }

        private static PerfCs[] PerforationUnited(PerfCs[] perforation, float zabIskMD, float pakerMD, DateTime ondate)
        {
            if (perforation is null)
                return null;
            if (ondate <= DateTime.Parse("1950-01-01"))
                ondate = DateTime.Now.Date;
            // свести перфорацию в массив с учетом перекрытия
            PerfCs[] perf1 = null;
            // первый проход. объединение перекрытий
            bool vhodExists = false;
            // P0- источник
            foreach (PerfCs p0 in perforation)
            {
                if (p0 is object && (p0.CloseDate <= DateTime.Parse("1950-01-01") | p0.CloseDate > ondate) & (p0.OpenDate <= DateTime.Parse("1950-01-01") | p0.OpenDate < ondate))
                {
                    if (perf1 is null)
                    {
                        perf1 = new PerfCs[1];
                        perf1[0] = new PerfCs
                        {
                            Top = p0.Top,
                            Baze = p0.Baze,
                            AOTop = p0.AOTop,
                            AOBaze = p0.AOBaze
                        };
                    }
                    else
                    {
                        vhodExists = false;
                        foreach (PerfCs iP in perf1)
                        {
                            if (iP is object)
                            {
                                if (iP.Top >= p0.Top & iP.Top <= p0.Baze | iP.Baze >= p0.Top & iP.Baze <= p0.Baze | iP.Top <= p0.Top & iP.Baze >= p0.Baze)

                                {
                                    // есть вхождение
                                    iP.Top = Math.Min(iP.Top, p0.Top);
                                    iP.Baze = Math.Max(iP.Baze, p0.Baze);
                                    vhodExists = true;
                                    break;
                                }
                            }
                        }

                        if (!vhodExists)
                        {
                            Array.Resize(ref perf1, perf1.Length + 1);
                            perf1[perf1.Length - 1] = new PerfCs
                            {
                                Top = p0.Top,
                                Baze = p0.Baze,
                                AOTop = p0.AOTop,
                                AOBaze = p0.AOBaze
                            };
                        }
                    }
                }
            }

            // второй проход, удаление дубликатов
            PerfCs[] perf2 = null;
            float[] mGlub = null; // массив вверхов интервалов перфорации для сортировки
            if (perf1 is object)
            {
                foreach (PerfCs p1 in perf1)
                {
                    if (perf2 is null)
                    {
                        perf2 = new PerfCs[1];
                        mGlub = new float[1];
                        perf2[0] = p1;
                        mGlub[0] = p1.Top;
                    }
                    else
                    {
                        bool exists = false;
                        foreach (PerfCs p2 in perf2)
                        {
                            if (p2.Top == p1.Top)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            Array.Resize(ref perf2, perf2.Length + 1);
                            Array.Resize(ref mGlub, mGlub.Length + 1);
                            perf2[perf2.Length - 1] = p1;
                            mGlub[mGlub.Length - 1] = p1.Top;
                        }
                    }
                }
            }
            // отсортируем массив по глубине
            PerfCs[] perf3 = null;
            int x = 0;
            if (perf2 is object)
            {
                Array.Sort(mGlub, perf2);
                // выборка интервалов выше искуственного забоя
                if (zabIskMD > 0f)
                {
                    for (int i = 0, loopTo = perf2.Length - 1; i <= loopTo; i++)
                    {
                        if (perf2[i] is object)
                        {
                            if (perf2[i].Top < zabIskMD)
                            {
                                Array.Resize(ref perf3, x + 1);
                                perf3[x] = new PerfCs
                                {
                                    Top = perf2[i].Top
                                };
                                if (perf2[i].Baze <= zabIskMD)
                                {
                                    perf3[x].Baze = perf2[i].Baze;
                                }
                                else
                                {
                                    perf3[x].Baze = zabIskMD;
                                    break;
                                }

                                x += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    perf3 = perf2;
                }
            }
            // выборка интервалов ниже пакера
            PerfCs[] perf4 = null;
            x = -1;
            if (perf3 is object)
            {
                // выборка интервалов выше искуственного забоя
                if (pakerMD > 0f)
                {
                    for (int i = 0, loopTo1 = perf3.Length - 1; i <= loopTo1; i++)
                    {
                        if (perf3[i] is object)
                        {
                            if (perf3[i].Top > pakerMD)
                            {
                                x += 1;
                                Array.Resize(ref perf4, x + 1);
                                perf4[x] = new PerfCs
                                {
                                    Top = perf3[i].Top,
                                    Baze = perf3[i].Baze
                                };
                            }
                            else if (perf3[i].Baze > pakerMD)
                            {
                                x += 1;
                                Array.Resize(ref perf4, x + 1);
                                perf4[x] = new PerfCs
                                {
                                    Top = pakerMD,
                                    Baze = perf3[i].Baze
                                };
                            }
                        }
                    }
                }
                else
                {
                    perf4 = perf3;
                }
            }

            return perf4;
        }
    }
}

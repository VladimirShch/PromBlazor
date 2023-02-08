using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.ApplicationLayer.Wells.Export;
using Web_Prom.Core.Blazor.Core.Entities.Wells;
using WellClass;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Export
{
    public class WellExportService : IWellExportService
    {
        private readonly Geolog.Contracts.IByArc _byArcService;
        private readonly UserCredentials _userCredentials;

        public WellExportService(Geolog.Contracts.IByArc byArcService, UserCredentials userCredentials)
        {
            _byArcService = byArcService;
            _userCredentials = userCredentials;
        }

        public async Task ExportWellList(IEnumerable<WellShort> wells, string selectedFieldId)
        {
            ItemList[]  wellsDto = wells.Select(t => new ItemList { CodeStr = t.Id, Name = t.Name}).ToArray();
            int fieldIdNumeric = int.Parse(selectedFieldId);
            
            byte[] wellsDtoBinary = ArhAndSer.ObjectToArchive(wellsDto);
            byte[] result = await _byArcService.LoadWellsInformationAsync(_userCredentials.Username, _userCredentials.Password, fieldIdNumeric, wellsDtoBinary);
            
            throw new NotImplementedException();
        }

        // Нарушение - метод не отсюда
        //private void SpravSkvajinALL_XLS(Well[] wells, string FiltrText)
        //{
        //    var selectDate = new SelectDateForm();
        //    var worskService = Web_Prom.Lib.GeologClient.Default.Get<Geolog.Contracts.ISprav>();

        //    selectDate.ShowDialog();
        //    var onDate = selectDate.DateTimeSelected;
            
        //    var wb = new XLTemplate(Application.StartupPath + _Shablon_XLS_SpavochnikScvajin);
        //    IXLWorksheet ws = wb.Workbook.Worksheet(1);
        //    IXLWorksheet ws2 = wb.Workbook.Worksheet(2);
        //    ws.Style.Font.SetFontSize(10);
        //    ws.Style.Font.SetFontName("Arial");
        //    ws2.Style.Font.SetFontSize(10);
        //    ws2.Style.Font.SetFontName("Arial");
        //    ws.Cell(2, 1).Value = "Справочник скважин. " + FiltrText;
        //    // Book.Sheets.Add("Ветрикальные отменки")

        //    int numRow;
        //    //int RowSheet1 = 8;
        //    numRow = 8;
        //    var n = default(int);
        //    var perfRow = default(int);
        //    if (wells is null)
        //        return;
        //    foreach (Well well in wells)
        //    {
        //        if (wells is object)
        //        {
        //            numRow += 2;
        //            n += 1;
        //            //IXLStyle xLStyle = ws.Range(numRow, 1, numRow, 35).Style;
        //            //var range = ws.Range(numRow, 1, numRow, 35).Style;
        //            //range.Style = XLWorkbook.DefaultStyle;
        //            //ws.Range(numRow+1, 1, numRow+1, 35).Style = xLStyle;
        //            //ws2.Range(numRow+1, 1, numRow+1, 35).Style = xLStyle;
        //            // порядковый номер
        //            ws.Cell(numRow, 1).Value = n;
        //            ws2.Cell(numRow, 1).Value = n;
        //            // объект эксплуатации
        //            if (well.Charac is object)
        //            {
        //                // номер скважины
        //                ws.Cell(numRow, 2).Value = MMAIN.DecimalString(well.Charac.Name, 1, "");
        //                ws2.Cell(numRow, 2).Value = MMAIN.DecimalString(well.Charac.Name, 1, "");
        //                // альтитуда
        //                ws.Cell(numRow, 4).Value = MMAIN.DecimalString(well.Charac.Alt, 1, "0.00");
        //                ws2.Cell(numRow, 4).Value = MMAIN.DecimalString(well.Charac.Alt, 1, "0.00");
        //                // забой
        //                ws.Cell(numRow, 5).Value = MMAIN.DecimalString(well.Charac.Zab.MD, 1, "0.0");
        //                if (well.Charac.Zab.TVD != -999 & well.Charac.Alt != -999)
        //                    ws2.Cell(numRow, 5).Value = MMAIN.DecimalString(well.Charac.Zab.TVD - well.Charac.Alt, 1, "0.00");
        //                // искуственный забой
        //                if (well.Prop.ZabIskNow is object)
        //                {
        //                    ws.Cell(numRow, 6).Value = MMAIN.DecimalString(well.Prop.ZabIskNow.MD, 1, "0.0");
        //                    if (well.Prop.ZabIskNow.TVD != -999 & well.Charac.Alt != -999)
        //                        ws2.Cell(numRow, 6).Value = MMAIN.DecimalString(well.Prop.ZabIskNow.TVD - well.Charac.Alt, 1, "0.00");
        //                }
        //                // Book.Sheets(0).Item(numRow, 5).Style.Format = "0.0"

        //                // окончание бурения
        //                if (well.works is object)
        //                {
        //                    ws.Cell(numRow, 30).Value = MMAIN.NullDate(well.works.Drilling.Date2);
        //                    ws2.Cell(numRow, 30).Value = MMAIN.NullDate(well.works.Drilling.Date2);
        //                }
        //                // дата приема на баланс
        //                ws.Cell(numRow, 31).Value = MMAIN.NullDate(well.Charac.OnBalans);
        //                ws2.Cell(numRow, 31).Value = MMAIN.NullDate(well.Charac.OnBalans);
        //                // дата пуска в эксплуатацию
        //                ws.Cell(numRow, 32).Value = MMAIN.NullDate(well.Charac.StartWork);
        //                ws2.Cell(numRow, 32).Value = MMAIN.NullDate(well.Charac.StartWork);
        //                // Book.Sheets(0).Item(numRow, 31).Style.Format = "dd.MM.yy"
        //            }

        //            if (well.Prop is object && well.Prop.ZabNow is object)
        //            {
        //                // текущий забой
        //                ws.Cell(numRow, 7).Value = MMAIN.DecimalString(well.Prop.ZabNow.MD, 1, "0.0");
        //                if (well.Prop.ZabNow.TVD != -999 & well.Charac.Alt != -999)
        //                    ws2.Cell(numRow, 7).Value = MMAIN.DecimalString(well.Prop.ZabNow.TVD - well.Charac.Alt, 1, "0.00");
        //                // дата замера текущего забоя
        //                ws.Cell(numRow, 8).Value = MMAIN.NullDate(well.Prop.ZabDate);
        //                ws2.Cell(numRow, 8).Value = MMAIN.NullDate(well.Prop.ZabDate);
        //            }

        //            if (PrintTypeState)
        //            {
        //                if (well.Prop is object)
        //                {
        //                    if (onDate.Date != DateAndTime.Now.Date.Date)
        //                    {
        //                        well.Prop.StatNow = worskService.GetWellStateOnDateAsync(well.UWI, onDate).Result;
        //                        well.Prop.TypeNow = worskService.GetWellTypeOnDateAsync(well.UWI, onDate).Result;
        //                    }

        //                    ws.Cell(numRow, 34).Value = MMAIN.GCodes.NameByCode(CODES.ТипСкважины, well.Prop.TypeNow, true);
        //                    ws2.Cell(numRow, 34).Value = MMAIN.GCodes.NameByCode(CODES.ТипСкважины, well.Prop.TypeNow, true);
        //                    ws.Cell(numRow, 35).Value = MMAIN.GCodes.NameByCode(CODES.СостояниеСкважины, well.Prop.StatNow, true);
        //                    ws2.Cell(numRow, 35).Value = MMAIN.GCodes.NameByCode(CODES.СостояниеСкважины, well.Prop.StatNow, true);
        //                }

        //                if (well.Charac is object)
        //                {
        //                    ws.Cell(numRow, 33).Value = MMAIN.GCodes.NameByCode(CODES.ТипСтвола, well.Charac.TypeStvol, true);
        //                    ws2.Cell(numRow, 33).Value = MMAIN.GCodes.NameByCode(CODES.ТипСтвола, well.Charac.TypeStvol, true);
        //                }
        //            }
        //            // конструкция
        //            int constractionMax = numRow;
        //            if (well.Constr is object)
        //            {
        //                {
        //                    var withBlock = well.Constr;
        //                    // направление
        //                    if (withBlock.Napr is object && withBlock.Napr.Enable(onDate))
        //                    {
        //                        int kRow = numRow;
        //                        bool firstrow = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.Napr.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow)
        //                                kRow += +1;
        //                            firstrow = false;
        //                            ws.Cell(kRow, 14).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow, 14).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow, 15).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow, 15).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow);
        //                    }
        //                    // кондуктор
        //                    if (withBlock.Cond is object && withBlock.Cond.Enable(onDate))
        //                    {
        //                        int kRow1 = numRow;
        //                        bool firstrow1 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.Cond.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow1)
        //                                kRow1 += +1;
        //                            firstrow1 = false;
        //                            ws.Cell(kRow1, 16).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow1, 16).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow1, 17).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow1, 17).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow1);
        //                    }
        //                    // техколонна
        //                    if (withBlock.TK is object && withBlock.TK.Enable(onDate))
        //                    {
        //                        int kRow2 = numRow;
        //                        bool firstrow2 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.TK.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow2)
        //                                kRow2 += +1;
        //                            firstrow2 = false;
        //                            ws.Cell(kRow2, 18).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow2, 18).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow2, 19).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow2, 19).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow2);
        //                    }

        //                    // эксплуатационная колонна доп.колонна
        //                    if (withBlock.EK is object && withBlock.EK.Enable(onDate))
        //                    {
        //                        int kRow3 = numRow;
        //                        bool firstrow3 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.EK.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow3)
        //                                kRow3 += +1;
        //                            firstrow3 = false;
        //                            ws.Cell(kRow3, 20).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow3, 20).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow3, 21).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow3, 21).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow3);
        //                    }
        //                    // эксплуатационная колонна доп.колонна
        //                    if (withBlock.EKDOP is object && withBlock.EKDOP.Enable(onDate))
        //                    {
        //                        int kRow4 = numRow;
        //                        bool firstrow4 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.EKDOP.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow4)
        //                                kRow4 += +1;
        //                            firstrow4 = false;
        //                            ws.Cell(kRow4, 22).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow4, 22).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow4, 23).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow4, 23).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow4);
        //                    }
        //                    int nKTRow = numRow;
        //                    // НКТ
        //                    if (withBlock.NKT is object && withBlock.NKT.Enable(onDate))
        //                    {
        //                        int kRow5 = numRow;
        //                        bool firstrow5 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.NKT.UnicDoutSections(onDate))
        //                        {
        //                            if (!firstrow5)
        //                                kRow5 += +1;
        //                            firstrow5 = false;
        //                            ws.Cell(kRow5, 24).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow5, 24).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow5, 25).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow5, 25).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow5);
        //                        nKTRow = kRow5 + 1;
        //                    }
        //                    if (withBlock.CNKT is not null && withBlock.CNKT.Enable(onDate))
        //                    {
        //                        int kRow6 = nKTRow;
        //                        bool firstrow6 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.CNKT.UnicDoutSections(onDate))
        //                        {
        //                            if (firstrow6)
        //                            {
        //                                ws.Cell(kRow6, 25).Value = "КЛК";
        //                                ws2.Cell(kRow6, 25).Value = "КЛК";
        //                            }
        //                            kRow6++;
        //                            firstrow6 = false;

        //                            ws.Cell(kRow6, 24).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws2.Cell(kRow6, 24).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0");
        //                            ws.Cell(kRow6, 25).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow6, 25).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow6 + 1);
        //                    }


        //                    // пакер
        //                    if (withBlock.Paker is object && Well.ConstrCs.LastEqvipment(withBlock.Paker, onDate) is object)
        //                    {
        //                        int kRow7 = numRow;
        //                        bool firstrow7 = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in Well.ConstrCs.LastEqvipment(withBlock.Paker, onDate))
        //                        {
        //                            if (!firstrow7)
        //                                kRow7 += +1;
        //                            firstrow7 = false;
        //                            ws.Cell(kRow7, 26).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(kRow7, 26).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                            // Book.Sheets(0).Item(PakerRow, 24).Style.Format = ""
        //                            string str1 = MMAIN.DecimalString(eqv.Code, 1, "0").ToString().Trim();
        //                            str1 = str1.Substring(str1.Length - 2);
        //                            ws.Cell(kRow7, 27).Value = MMAIN.DecimalString(str1, 1, "0");
        //                            ws2.Cell(kRow7, 27).Value = MMAIN.DecimalString(str1, 1, "0");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, kRow7);
        //                    }

        //                    // тип колонной головки
        //                    if (withBlock.KG is object && Well.ConstrCs.LastEqvipment(withBlock.KG, onDate) is object)
        //                    {
        //                        int KRow = numRow;
        //                        bool Firstrow = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in Well.ConstrCs.LastEqvipment(withBlock.KG, onDate))
        //                        {
        //                            if (!Firstrow)
        //                                KRow += +1;
        //                            Firstrow = false;
        //                            string Str = MMAIN.DecimalString(eqv.Code, 1, "0").ToString().Trim();
        //                            Str = Str.Substring(Str.Length - 2);
        //                            ws.Cell(KRow, 28).Value = MMAIN.DecimalString(Str, 1, "0");
        //                            ws2.Cell(KRow, 28).Value = MMAIN.DecimalString(Str, 1, "0");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, KRow);
        //                    }
        //                    // тип фонтанной арматуры
        //                    if (withBlock.FA is object && Well.ConstrCs.LastEqvipment(withBlock.FA, onDate) is object)
        //                    {
        //                        int KRow = numRow;
        //                        bool Firstrow = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in Well.ConstrCs.LastEqvipment(withBlock.FA, onDate))
        //                        {
        //                            if (!Firstrow)
        //                                KRow += +1;
        //                            Firstrow = false;
        //                            string Str = MMAIN.DecimalString(eqv.Code, 1, "0").ToString().Trim();
        //                            Str = Str.Substring(Str.Length - 2);
        //                            ws.Cell(KRow, 29).Value = MMAIN.DecimalString(Str, 1, "0");
        //                            ws2.Cell(KRow, 29).Value = MMAIN.DecimalString(Str, 1, "0");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, KRow);
        //                    }
        //                    // фильтр 
        //                    if (withBlock.Filtr is object && withBlock.Filtr.Enable(onDate))
        //                    {
        //                        int KRow = numRow;
        //                        bool Firstrow = true;
        //                        foreach (Well.ConstrCs.EqCs eqv in withBlock.Filtr.Sections(onDate))
        //                        {
        //                            if (!Firstrow)
        //                                KRow += +1;
        //                            Firstrow = false;
        //                            ws.Cell(KRow, 11).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0.0");
        //                            ws2.Cell(KRow, 11).Value = MMAIN.DecimalString(eqv.dOUT, 1, "0.0");
        //                            int Fpoint = eqv.Where;
        //                            if (Fpoint > 0)
        //                            {
        //                                string pointName = "";
        //                                switch (Fpoint)
        //                                {
        //                                    case var @case when @case == CODES.Construction.ЭксплуатационнаяКолонна:
        //                                        {
        //                                            pointName = "ЭК";
        //                                            break;
        //                                        }

        //                                    case var case1 when case1 == CODES.Construction.ЭксплКолоннааДополнительная:
        //                                        {
        //                                            pointName = "допЭК";
        //                                            break;
        //                                        }

        //                                    case var case2 when case2 == CODES.Construction.НКТ1:
        //                                        {
        //                                            pointName = "НКТ";
        //                                            break;
        //                                        }

        //                                    case var case4 when case4 == CODES.Construction.ТехКолонна:
        //                                        {
        //                                            pointName = "ТК";
        //                                            break;
        //                                        }

        //                                    default:
        //                                        {
        //                                            pointName = "неопр.";
        //                                            break;
        //                                        }
        //                                }

        //                                ws.Cell(KRow, 11).Value += "/" + pointName;
        //                                ws2.Cell(KRow, 11).Value += "/" + pointName;
        //                            }

        //                            var Plasts = Well.PlastsByInterval(well.Vskrut, eqv.Top, eqv.Baze);
        //                            string str = MMAIN.WellListForWellList.get_PlastObjNameByCOdes(Plasts, ";");
        //                            ws.Cell(KRow, 12).Value = str;
        //                            ws2.Cell(KRow, 12).Value = str;
        //                            ws.Cell(KRow, 9).Value = MMAIN.DecimalString(eqv.Top, 1, "0.0");
        //                            ws2.Cell(KRow, 9).Value = MMAIN.DecimalString(eqv.AOTOp, 1, "0.00");
        //                            ws.Cell(KRow, 10).Value = MMAIN.DecimalString(eqv.Baze, 1, "0.0");
        //                            ws2.Cell(KRow, 10).Value = MMAIN.DecimalString(eqv.AOBaze, 1, "0.00");
        //                            ws.Cell(KRow, 13).Value = MMAIN.DecimalString(eqv.Length, 1, "0.00");
        //                            ws2.Cell(KRow, 13).Value = MMAIN.DecimalString(eqv.AOBaze - eqv.AOTOp, 1, "0.00");
        //                        }

        //                        constractionMax = Math.Max(constractionMax, KRow);
        //                        if (ws.Cell(KRow, 9).Value.ToString() != "-" & ws.Cell(KRow, 10).Value.ToString() != "-")
        //                        {
        //                            perfRow = KRow + 1;
        //                        }
        //                        else
        //                        {
        //                            perfRow = KRow;
        //                        }
        //                    }
        //                }
        //            }
        //            // перфорация
        //            if (perfRow < numRow)
        //                perfRow = numRow;
        //            var ZBISK_ST = default(float);
        //            var ZBISK_VRT = default(float);
        //            if (well.Prop.ZabIskNow is object)
        //            {
        //                ZBISK_ST = well.Prop.ZabIskNow.MD;
        //                ZBISK_VRT = well.Prop.ZabIskNow.TVD;
        //            }

        //            if (well.Perf is object & well.PerforationUnited(ZBISK_ST, 0f, onDate) is object)
        //            {
        //                bool PerfWrited = false;
        //                foreach (Well.PerfCs perf in well.Perf)
        //                {
        //                    if (perf is object && perf.CloseDate <= DateTime.Parse("1950-01-01") && perf.OpenDate < onDate)
        //                    {
        //                        if (perf.Top < ZBISK_ST | ZBISK_ST <= 0f)
        //                        {
        //                            if (PerfWrited)
        //                                perfRow += 1;
        //                            ws.Cell(perfRow, 9).Value = MMAIN.DecimalString(perf.Top, 1, "0.0");
        //                            ws2.Cell(perfRow, 9).Value = MMAIN.DecimalString(perf.AOTop, 1, "0.00");
        //                            ws.Cell(perfRow, 10).Value = MMAIN.DecimalString(perf.Baze, 1, "0.0");
        //                            ws2.Cell(perfRow, 10).Value = MMAIN.DecimalString(perf.AOBaze, 1, "0.00");
        //                            if (perf.Baze > ZBISK_ST & ZBISK_ST > 0f)
        //                            {
        //                                ws.Cell(perfRow, 10).Value = MMAIN.DecimalString(ZBISK_ST, 1, "0.0");
        //                                ws2.Cell(perfRow, 10).Value = MMAIN.DecimalString(ZBISK_VRT - well.Charac.Alt, 1, "0.00");
        //                            }

        //                            var Plasts = Well.PlastsByInterval(well.Vskrut, perf.Top, perf.Baze);
        //                            string str = MMAIN.WellListForWellList.get_PlastObjNameByCOdes(Plasts, ";");
        //                            ws.Cell(perfRow, 12).Value = str;
        //                            ws2.Cell(perfRow, 12).Value = str;
        //                            ws.Cell(perfRow, 13).Value = MMAIN.DecimalString(perf.dH, 1, "0.0");
        //                            ws2.Cell(perfRow, 13).Value = MMAIN.DecimalString(perf.AOdH, 1, "0.00");
        //                            PerfWrited = true;
        //                        }
        //                    }
        //                }
        //            }

        //            numRow = Math.Max(numRow, constractionMax);
        //            numRow = Math.Max(numRow, perfRow);
        //            // numRow = Math.Max(numRow, FiltrRow)

        //        }
        //    }
        //    ws.Range(10, 4, numRow, 4).Style.NumberFormat.Format = "0.00";
        //    ws.Range(10, 5, numRow, 7).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 8, numRow, 8).Style.NumberFormat.Format = "dd.mm.yy";
        //    ws.Range(10, 9, numRow, 11).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 13, numRow, 13).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 15, numRow, 15).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 17, numRow, 17).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 19, numRow, 19).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 21, numRow, 23).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 25, numRow, 26).Style.NumberFormat.Format = "0.0";
        //    ws.Range(10, 30, numRow, 32).Style.NumberFormat.Format = "dd.mm.yy";

        //    ws2.Range(10, 4, numRow, 7).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 8, numRow, 8).Style.NumberFormat.Format = "dd.mm.yy";
        //    ws2.Range(10, 9, numRow, 10).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 11, numRow, 11).Style.NumberFormat.Format = "0.0";
        //    ws2.Range(10, 13, numRow, 13).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 15, numRow, 15).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 17, numRow, 17).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 19, numRow, 19).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 21, numRow, 23).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 25, numRow, 26).Style.NumberFormat.Format = "0.00";
        //    ws2.Range(10, 30, numRow, 32).Style.NumberFormat.Format = "dd.mm.yy";

        //    // добавим коды
        //    numRow += 1;
        //    var tab = MMAIN.GCodes.Table(CODES.Construction.ФонтаннаяАрматура);
        //    // фонтанные арматуры
        //    bool FirstR = true;
        //    foreach (DataRow DataROW in tab.Select("GRP=" + CODES.Construction.ФонтаннаяАрматура))
        //    {
        //        numRow += 1;
        //        if (FirstR)
        //        {
        //            ws.Cell(numRow, 2).Value = DataROW[2];
        //            ws2.Cell(numRow, 2).Value = DataROW[2];
        //            FirstR = false;
        //        }
        //        else
        //        {
        //            string Str = DataROW[0].ToString().Trim();
        //            Str = Str.Substring(Str.Length - 2);
        //            ws.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //            ws2.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //        }
        //    }
        //    // Типы колонных головок
        //    FirstR = true;
        //    foreach (DataRow DataROW in tab.Select("GRP=" + CODES.Construction.КолоннаяГоловка))
        //    {
        //        numRow += 1;
        //        if (FirstR)
        //        {
        //            ws.Cell(numRow, 2).Value = DataROW[2];
        //            ws2.Cell(numRow, 2).Value = DataROW[2];
        //            FirstR = false;
        //        }
        //        else
        //        {
        //            string Str = DataROW[0].ToString().Trim();
        //            Str = Str.Substring(Str.Length - 2);
        //            ws.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //            ws2.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //        }
        //    }
        //    // Типы пакеров
        //    FirstR = true;
        //    foreach (DataRow DataROW in tab.Select("GRP=" + CODES.Construction.Пакер))
        //    {
        //        numRow += 1;
        //        if (FirstR)
        //        {
        //            ws.Cell(numRow, 2).Value = DataROW[2];
        //            ws2.Cell(numRow, 2).Value = DataROW[2];
        //            FirstR = false;
        //        }
        //        else
        //        {
        //            string Str = DataROW[0].ToString().Trim();
        //            Str = Str.Substring(Str.Length - 2);
        //            ws.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //            ws2.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //        }
        //    }
        //    // Типы клапанов отсекателей
        //    FirstR = true;
        //    foreach (DataRow DataROW in tab.Select("GRP=" + CODES.Construction.КлапагОтсекатель))
        //    {
        //        numRow += 1;
        //        if (FirstR)
        //        {
        //            ws.Cell(numRow, 2).Value = DataROW[2];
        //            ws2.Cell(numRow, 2).Value = DataROW[2];
        //            FirstR = false;
        //        }
        //        else
        //        {
        //            string Str = DataROW[0].ToString().Trim();
        //            Str = Str.Substring(Str.Length - 2);
        //            ws.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //            ws2.Cell(numRow, 2).Value = Operators.ConcatenateObject(Str + "  -  ", DataROW[4]);
        //        }
        //    }

        //    string ondatestring = DateAndTime.Now.Date.Date != onDate.Date ? ("на_" + onDate.ToString("ddMMyyyy") + "_") : "";
        //    //string fileName = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(PathResult + "Справочник_Скважин_", ondatestring), DateAndTime.Now.Year), "_"), DateAndTime.Now.Month), "_"), DateAndTime.Now.Day), "-"), DateAndTime.Now.Hour), "_"), DateAndTime.Now.Minute), ".xlsx"));
        //    string fileName = PathResult + "Справочник_Скважин_" + ondatestring + DateAndTime.Now.Year.ToString() + "_" + DateAndTime.Now.Month.ToString() + "_" + DateAndTime.Now.Day.ToString() + "_" + DateAndTime.Now.Hour.ToString() + "_" + DateAndTime.Now.Minute.ToString() + ".xlsx";
        //    wb.SaveAs(fileName);
        //    if (System.IO.File.Exists(fileName))
        //    {
        //        Process.Start(fileName);
        //    }
        //}

    }
}

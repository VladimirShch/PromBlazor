using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;

namespace Web_Prom.Core.Blazor.DataAccess.WellConstructions
{
    public class WellConstructionAdapter : IWellConstructionAdapter
    {
        public ICollection<WellConstructionItem> Convert(WellClass.Well.ConstrCs constructionDto)
        {
            var construction = new List<WellConstructionItem>();
            
            if (constructionDto is null)
            {
                return construction;
            }

            AddConstructionItems(construction, constructionDto.FA);
            AddConstructionItems(construction, constructionDto.KG);
            AddConstructionItems(construction, constructionDto.KO);
            // Не сортированные!!!
            AddConstructionItems(construction, constructionDto?.Napr?.SectionsList);
            AddConstructionItems(construction, constructionDto?.Cond?.SectionsList);
            AddConstructionItems(construction, constructionDto?.TK?.SectionsList);
            AddConstructionItems(construction, constructionDto?.EK?.SectionsList);
            AddConstructionItems(construction, constructionDto?.EKDOP?.SectionsList);
            AddConstructionItems(construction, constructionDto?.NKT?.SectionsList);
            AddConstructionItems(construction, constructionDto?.CNKT?.SectionsList);

            AddConstructionItems(construction, constructionDto?.Paker);
            AddConstructionItems(construction, constructionDto?.Filtr?.SectionsList);

            AddConstructionItems(construction, constructionDto?.Cement);
            AddConstructionItems(construction, constructionDto?.Postor);

            return construction;
        }

        public WellClass.Well.ConstrCs ConvertBack(ICollection<WellConstructionItem> constructionItems, IEnumerable<EquipmentType> equipmentTypes)
        {
            WellClass.Well.ConstrCs constructionDto = new();

            foreach(WellConstructionItem wellConstructionItem in constructionItems)
            {
                WellEquipmentKind? itemKind = equipmentTypes.FirstOrDefault(t => t.Id == wellConstructionItem.Code)?.Kind;
                WellClass.Well.ConstrCs.EqCs constructionItemDto = ConvertItemBack(wellConstructionItem);
                switch (itemKind)
                {
                    case WellEquipmentKind.WellheadEquipment:
                        constructionDto.AddFA(constructionItemDto);
                        break;
                    case WellEquipmentKind.CasingHead:
                        constructionDto.AddKG(constructionItemDto);
                        break;
                    case WellEquipmentKind.SafetyValve:
                        constructionDto.AddKO(constructionItemDto);
                        break;
                    case WellEquipmentKind.Packer:
                        constructionDto.AddPaker(constructionItemDto);
                        break;
                    case WellEquipmentKind.CementPlug:
                        constructionDto.AddCM(constructionItemDto);
                        break;                 
                    case WellEquipmentKind.SurfaceCasing:
                        if (constructionDto.Cond is null)
                            constructionDto.Cond = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.Cond.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.Casing:
                        if (constructionDto.EK is null)
                            constructionDto.EK = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.EK.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.CasingAdditional:
                        if (constructionDto.EKDOP is null)
                            constructionDto.EKDOP = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.EKDOP.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.Filter:
                        if (constructionDto.Filtr is null)
                            constructionDto.Filtr = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.Filtr.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.Conductor:
                        if (constructionDto.Napr is null)
                            constructionDto.Napr = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.Napr.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.Tubing:
                        if (constructionDto.NKT is null)
                            constructionDto.NKT = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.NKT.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.ConcentricTubing:
                        if (constructionDto.CNKT is null)
                            constructionDto.CNKT = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.CNKT.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.IntermediateCasing:
                        if (constructionDto.TK is null)
                            constructionDto.TK = new WellClass.Well.ConstrCs.TrubaCl();
                        constructionDto.TK.SectionAdd(constructionItemDto);
                        break;
                    case WellEquipmentKind.Other:
                        constructionDto.AddPP(constructionItemDto);
                        break;
                }
            }

            return constructionDto;
        }

        private void AddConstructionItems(List<WellConstructionItem> construction, List<WellClass.Well.ConstrCs.EqCs>? equipmentDtoItems)
        {
            if (equipmentDtoItems is not null)
            {
                IEnumerable<WellConstructionItem> faConstructionItems = equipmentDtoItems.Where(t => t is not null).Select(ConvertItem);
                construction.AddRange(faConstructionItems);
            }
        }

        private WellConstructionItem ConvertItem(WellClass.Well.ConstrCs.EqCs equipmentDto)
        {
            if(equipmentDto is null)
            {
                throw new ArgumentNullException(nameof(equipmentDto));
            }

            var constructionItem = new WellConstructionItem
            {
                Id = equipmentDto.ID,
                Code = equipmentDto.Code,
                //Type = equipmentTypes?.FirstOrDefault(t => t.Id == equipmentDto.Code)?.Kind ?? WellEquipmentKind.Unknown,
                //Name = equipmentTypes?.FirstOrDefault(t => t.Id == equipmentDto.Code)?.Name ?? string.Empty, //equipmentDto.Name, // Но наименование на самом деле - Constr.Code
                InstallationDate = equipmentDto.Inst,
                RemovementDate = equipmentDto.Uninst,
                Top = equipmentDto.Top,
                Base = equipmentDto.Baze,
                TopAbsolute = equipmentDto.AOTOp,
                BaseAbsolute = equipmentDto.AOBaze,
                OuterDiameter = equipmentDto.dOUT,
                InnerDiameter = equipmentDto.dIN,
                Thickness = equipmentDto.Thick,
                Connection = equipmentDto.Where,
                Remark = equipmentDto.Note
            };

            return constructionItem;
        }
        private WellClass.Well.ConstrCs.EqCs ConvertItemBack(WellConstructionItem constructionItem)
        {
            if (constructionItem is null)
            {
                throw new ArgumentNullException(nameof(constructionItem));
            }

            var equipmentDto = new WellClass.Well.ConstrCs.EqCs
            {
                ID = constructionItem.Id,
                Code = constructionItem.Code,
                Inst = constructionItem.InstallationDate,
                Uninst = constructionItem.RemovementDate,
                Top = constructionItem.Top,
                Baze = constructionItem.Base,
                dOUT = constructionItem.OuterDiameter,
                dIN = constructionItem.InnerDiameter,
                Thick = constructionItem.Thickness,
                Where = constructionItem.Connection,
                Note = constructionItem.Remark
            };
          
            return equipmentDto;
        }
    }
}

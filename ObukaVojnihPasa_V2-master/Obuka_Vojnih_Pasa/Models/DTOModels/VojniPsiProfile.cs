using AutoMapper;
using Obuka_Vojnih_Pasa.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Models.DTOModels
{
    public class VojniPsiProfile:Profile
    {
        public VojniPsiProfile()
        {
            //get
            CreateMap<Obuka, ObukaDTO>().ForMember(o=>o.BrojAngazovanihInstruktora, map=>map.MapFrom(o=>o.Instruktori.Count())).ForMember(o=>o.BrojPasaNaObuci,map=>map.MapFrom(o=>o.Psi.Count()));
            CreateMap<Zadatak, ZadatakDTO>().ForMember(z=>z.UkupanBrojAngazovanja, map=>map.MapFrom(z=>z.Angazovanja.Count()));
           CreateMap<Instruktor,InstruktorDTO>().ForMember(p => p.Naziv, map => map.MapFrom(p => p.Obuka.Naziv))
            .ForMember(p => p.Trajanje, map => map.MapFrom(p => p.Obuka.Trajanje))
            .ForMember(p => p.Opis, map => map.MapFrom(p => p.Obuka.Opis))
           .ForMember(o => o.BrojAngazovanihInstruktora, map => map.MapFrom(o => o.Obuka.Instruktori.Count())).ForMember(o => o.BrojPasaNaObuci, map => map.MapFrom(o => o.Obuka.Psi.Count()));
            CreateMap<Pas, PasDTO>().ForMember(p => p.Naziv, map => map.MapFrom(p => p.Obuka.Naziv))
            .ForMember(p => p.Trajanje, map => map.MapFrom(p => p.Obuka.Trajanje))
            .ForMember(p => p.Opis, map => map.MapFrom(p => p.Obuka.Opis))
            .ForMember(p=>p.UkupanBrojAngazovanja, map=>map.MapFrom(p=>p.Angazovanja.Count()));
            CreateMap<Angazovanje, AngazovanjaDTO>().ForMember(a => a.NazivZadatka, map => map.MapFrom(a => a.Zadatak.NazivZadatka))
     .ForMember(a => a.Teren, map => map.MapFrom(a => a.Zadatak.Teren))
     .ForMember(a => a.Datum, map => map.MapFrom(a => a.Zadatak.Datum))
     .ForMember(a=>a.DatumRodjenja, map=> map.MapFrom(a=>a.Pas.DatumRodjenja))
          .ForMember(a => a.BrojZdravstveneKnjizice, map => map.MapFrom(a => a.Pas.BrojZdravstveneKnjizice))
               .ForMember(a => a.Ime, map => map.MapFrom(a => a.Pas.Ime))
               .ForMember(a => a.Rasa, map => map.MapFrom(a => a.Pas.Rasa))
             .ForMember(a => a.Pol, map => map.MapFrom(a => a.Pas.Pol));

            //put,post
            CreateMap<ObukaDTO, Obuka>();
            CreateMap<PasDTO, Pas>();
            CreateMap<ZadatakDTO, Zadatak>();
            CreateMap<InstruktorDTO, Instruktor>();
        }
    }
}

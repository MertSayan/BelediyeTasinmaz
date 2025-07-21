using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constans
{
    public static class Messages<T>
    {
        public static readonly string EntityName=typeof(T).Name;

        public static string EntityAdded => $"{EntityName} nesnesi başarıyla eklendi";
        public static string EntitiesAdded => $"{EntityName} nesneleri başarıyla eklendi";
        public static string EntityUpdated => $"{EntityName} nesnesi başarıyla güncellendi";
        public static string EntityDeleted => $"{EntityName} nesnesi başarıyla silindi";
        public static string EntityNotFound => $"Girdiğiniz id değerine sahip bir {EntityName} nesnesi bulunamadı";
        public static string EntityNameDuplicated => $"Bu isme sahip bir {EntityName} nesnesi zaten var, lütfen ismini değiştirin";
        public static string EntityCantMatches => $"Email veya sifre uyusmuyor";
        public static string EntityCantGet => $"{EntityName} alanına ait veri bulunamadı.";
    }
}

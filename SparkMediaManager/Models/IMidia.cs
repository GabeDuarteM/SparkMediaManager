// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 14:21

namespace SparkMediaManager.Models
{
    public interface IMidia
    {
        int IntCodigo { get; set; }

        string StrTitulo { get; set; }

        string StrSinopse { get; set; }

        string StrBackground { get; set; }

        string StrPoster { get; set; }

        byte[] BytCacheBackground { get; set; }

        byte[] BytCachePoster { get; set; }

        string StrPasta { get; set; }

        string StrMetadata { get; set; }

        string StrFormatoRenomeioPersonalizado { get; set; }

        //ObservableCollection<SerieAlias> _lstSerieAlias;

        void SetPastaPadrao();
    }
}

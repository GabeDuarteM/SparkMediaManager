// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 14:21

namespace SparkMediaManager.Models
{
    public interface IMidia
    {
        byte[] BytCacheBackground { get; set; }

        byte[] BytCachePoster { get; set; }

        int IntCodigo { get; set; }

        string StrBackground { get; set; }

        string StrFormatoRenomeioPersonalizado { get; set; }

        string StrMetadata { get; set; }

        string StrPasta { get; set; }

        string StrPoster { get; set; }

        string StrSinopse { get; set; }

        string StrTitulo { get; set; }

        //ObservableCollection<SerieAlias> _lstSerieAlias;

        void SetPastaPadrao();
    }
}

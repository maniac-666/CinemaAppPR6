namespace CinemaApp
{
    public static class Core
    {
        public static CinemaDBEntities Context = new CinemaDBEntities();
        public static Users CurrentUser = null;
    }
}

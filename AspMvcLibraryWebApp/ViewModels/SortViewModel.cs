using AspMvcLibraryWebApp.Controllers;

namespace AspMvcLibraryWebApp.ViewModels
{
    public class SortViewModel
    {
        public SortState TitleState { get; set; }
        public SortState AuthorState { get; set; }
        public SortState GenreState { get; set; }
        public SortState YearState { get; set; }
        public SortState CurrentState { get; set; }

        public SortViewModel(SortState sortState)
        {
            TitleState = SortState.TitleAsc;
            AuthorState = SortState.AuthorAsc;
            GenreState = SortState.GenreAsc;
            YearState = SortState.YearAsc;

            CurrentState = sortState;

            switch (sortState)
            {
                case SortState.TitleDesc:
                    CurrentState = TitleState = SortState.TitleAsc;
                    break;
                case SortState.AuthorAsc:
                    CurrentState = AuthorState = SortState.AuthorDesc;
                    break;
                case SortState.AuthorDesc:
                    CurrentState = AuthorState = SortState.AuthorAsc;
                    break;
                case SortState.GenreAsc:
                    CurrentState = GenreState = SortState.GenreDesc;
                    break;
                case SortState.GenreDesc:
                    CurrentState = GenreState = SortState.GenreAsc;
                    break;
                case SortState.YearAsc:
                    CurrentState = YearState = SortState.YearDesc;
                    break;
                case SortState.YearDesc:
                    CurrentState = YearState = SortState.YearAsc;
                    break;
                default:
                    CurrentState = TitleState = SortState.TitleDesc;
                    break;
            }

        }
    }
}

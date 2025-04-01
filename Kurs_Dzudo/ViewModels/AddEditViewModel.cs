using System;
using System.Reactive;
using Kurs_Dzudo.Hardik.Connector.Date;
using ReactiveUI;

namespace Kurs_Dzudo.ViewModels
{
    public class AddEditViewModel : ReactiveObject
    {
        private UkhasnikiDao _currentParticipant;
        private bool _isEditMode;
        private string _windowTitle;

        public AddEditViewModel()
        {
            CurrentParticipant = new UkhasnikiDao();
            IsEditMode = false;
            WindowTitle = "Добавить участника";

            SaveCommand = ReactiveCommand.Create(Save);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }

        public AddEditViewModel(UkhasnikiDao participant) : this()
        {
            CurrentParticipant = participant;
            IsEditMode = true;
            WindowTitle = "Редактировать участника";
        }

        public UkhasnikiDao CurrentParticipant
        {
            get => _currentParticipant;
            set => this.RaiseAndSetIfChanged(ref _currentParticipant, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set => this.RaiseAndSetIfChanged(ref _windowTitle, value);
        }

        public DateTimeOffset CompetitionDate
        {
            get => new DateTimeOffset(CurrentParticipant.DateSorevnovaniy.Year,
                                    CurrentParticipant.DateSorevnovaniy.Month,
                                    CurrentParticipant.DateSorevnovaniy.Day,
                                    0, 0, 0, TimeSpan.Zero);
            set => CurrentParticipant.DateSorevnovaniy = new DateOnly(value.Year, value.Month, value.Day);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        private void Save()
        {
            // Validation can be added here
            Close(true);
        }

        private void Cancel()
        {
            Close(false);
        }

        public event Action<bool> CloseWindow;

        private void Close(bool result)
        {
            CloseWindow?.Invoke(result);
        }
    }
}
using ContactManager.Model;
using ContactManager.Views;

namespace ContactManager.Presenters
{
    public class EditContactPresenter : PresenterBase<EditContactView>
    {
        private readonly ApplicationPresenter _appPresenter;
        private readonly Contact _contact;

        public EditContactPresenter(
            ApplicationPresenter appPresenter,
            EditContactView view,
            Contact contact)
            : base(view, "Contact.LookupName")
        {
            _appPresenter = appPresenter;
            _contact = contact;
        }

        public Contact Contact
        {
            get { return _contact; }
        }

        public void SelectImage()
        {
            string imagePath = View.AskUserForImagePath();
            if (!string.IsNullOrEmpty(imagePath))
            {
                Contact.ImagePath = imagePath;
            }
        }

        public void Save()
        {
            _appPresenter.SaveContact(Contact);
        }

        public void Delete()
        {
            _appPresenter.CloseTab(this);
            _appPresenter.DeleteContact(Contact);
        }

        public void Close()
        {
            _appPresenter.CloseTab(this);
        }

        public override bool Equals(object obj)
        {
            EditContactPresenter presenter = obj as EditContactPresenter;
            return presenter != null && presenter.Contact.Equals(Contact);
        }
    }
}

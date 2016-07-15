using System;
using System.Collections.Generic;
using ContactManager.Views;
using System.Collections.ObjectModel;
using ContactManager.Model;

namespace ContactManager.Presenters
{
    public class ContactListPresenter : PresenterBase<ContactListView>
    {
        private readonly ApplicationPresenter _appPresenter;

        public ObservableCollection<Contact> AllContacts
        {
            get { return _appPresenter.CurrentContacts; }
        }

        public ContactListPresenter(
            ApplicationPresenter appPresenter,
            ContactListView view)
            : base(view, "TabHeader")
        {
            _appPresenter = appPresenter;
        }

        public string TabHeader
        {
            get { return "All Contacts"; }
        }

        public void Close()
        {
            _appPresenter.CloseTab(this);
        }

        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public void OpenContact(Contact contact)
        {
            _appPresenter.OpenContact(contact);
        }
    }
}

using System;
using System.Collections.Generic;

namespace ContactManager.Model
{
    /// <summary>
    /// 数据存取接口
    /// </summary>
    public interface IContactRepository
    {
        void Save(Contact contact);

        void Delete(Contact contact);

        List<Contact> FindByLookup(string lookupName);

        List<Contact> FindAll();
    }
}

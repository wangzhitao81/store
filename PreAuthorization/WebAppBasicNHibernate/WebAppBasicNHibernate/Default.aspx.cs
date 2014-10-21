using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppBasicNHibernate.Vo;
using WebAppBasicNHibernate.Dao;
using log4net;
using NHibernate;
using WebAppBasicNHibernate.NHibernate;

namespace WebAppBasicNHibernate
{
    public partial class Default : System.Web.UI.Page
    {
        private static ILog _log = LogManager.GetLogger(Log4NetConstants.WEB_APP_BASIC_NHIBERNATE_LOGGER);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ManipulatePerson()
        {
            try
            {
                SessionHelper sessionHelper = new SessionHelper();
                using (ITransaction transaction = sessionHelper.Current.BeginTransaction())
                {
                    PersonDao personDao = new PersonDao();
                    // remove John and Mary
                    personDao.DeleteByName("John");
                    personDao.DeleteByName("Mary");

                    _log.Info("Removed John and Mary, in case they do exist");
                    lblOperations.Text = "Removed John and Mary, in case they do exist";

                    // create john
                    Person john = new Person();
                    john.Name = "John";
                    john.Age = 32;

                    personDao.SaveOrUpdate(john);

                    _log.Info("Created John.");
                    lblOperations.Text += "<br>Created John.";

                    // create mary
                    Person mary = new Person();
                    mary.Name = "Mary";
                    mary.Age = 33;

                    personDao.SaveOrUpdate(mary);

                    _log.Info("Created Mary.");
                    lblOperations.Text += "<br>Created Mary.";

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // log exception in case any errors uccur
                _log.Error("Error while manipulating entities using NHibernate.", ex);
            }
        }

        protected void btnExecuteNHibernateOperations_Click(object sender, EventArgs e)
        {
            // execute operations with NHibernate
            ManipulatePerson();
        }
    }
}
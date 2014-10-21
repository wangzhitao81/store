using System;
using System.Collections.Generic;
using System.Transactions;
using log4net;
using log4net.Config;
using WebAppBasicNHibernate.Dao;
using WebAppBasicNHibernate.NHibernate;
using WebAppBasicNHibernate.Vo;

namespace WebAppBasicNHibernate
{
    public class Global : System.Web.HttpApplication
    {
        protected SessionHelper _sessionHelper = null;

        private static ILog _log = LogManager.GetLogger(Log4NetConstants.WEB_APP_BASIC_NHIBERNATE_LOGGER);

        protected void Application_Start(object sender, EventArgs e)
        {
            // start log4net
            XmlConfigurator.Configure();

            try
            {

                _sessionHelper = new SessionHelper();
                _sessionHelper.OpenSession();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    PersonDao personDao = new PersonDao();

                    // delete all people, in case there are any
                    IList<Person> people = personDao.LoadAll();
                    if (people != null && people.Count > 0)
                    {
                        foreach (Person person in people)
                        {
                            personDao.Delete(person);
                        }
                    }

                    // create three people
                    Person jose = new Person();
                    jose.Name = "Jose";
                    jose.Age = 28;
                    personDao.SaveOrUpdate(jose);

                    Person maria = new Person();
                    maria.Name = "Maria";
                    maria.Age = 29;
                    personDao.SaveOrUpdate(maria);

                    Person mario = new Person();
                    mario.Name = "Mario";
                    mario.Age = 27;
                    personDao.SaveOrUpdate(mario);

                    // delete Mario
                    personDao.Delete(mario);

                    transactionScope.Complete();
                }

                _sessionHelper.CloseSession();

            }
            catch (Exception ex)
            {
                _log.Error("An error has occured while initializing the application.", ex);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            _sessionHelper = new SessionHelper();
            _sessionHelper.OpenSession();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            _sessionHelper = new SessionHelper();
            _sessionHelper.CloseSession();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
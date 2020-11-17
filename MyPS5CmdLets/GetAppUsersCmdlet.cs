using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;



namespace MyPS5CmdLets
{
    [Cmdlet(VerbsCommon.Get, "AppUsers")]
    [OutputType(typeof(AppUsers))]
    public class GetAppUsersCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int? Id { get; set; }
        [Parameter(Position = 1)]
        public string Email { get; set; }
        [Parameter(Position = 2)]
        public string UserName { get; set; }
        private IEnumerable<AppUsers> _appUsers;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _appUsers=GetAppUsers();
           
            
        }

        private static IEnumerable<AppUsers> GetAppUsers()
        {
            List<AppUsers> appUsers = new List<AppUsers>();

            appUsers.Add(new AppUsers
            {
                Email="Patryk@U.com",
                Id=0,
                UserName="Patryk"
            });

            appUsers.Add(new AppUsers
            {
                Email = "Piotr@U.com",
                Id = 1,
                UserName = "Piotr"
            });

            appUsers.Add(new AppUsers
            {
                Email = "Dawid@U.com",
                Id = 2,
                UserName = "Dawid"
            });

            appUsers.Add(new AppUsers
            {
                Email = "Pawel@U.com",
                Id = 3,
                UserName = "Pawel"
            });

            return appUsers;
        }

        protected override void ProcessRecord()
        {
            var query = _appUsers;

            if (UserName != null)
            {
                query = query.Where(x => x.UserName.StartsWith(UserName));
            }

            if (Email!=null)
            {
                query = query.Where(x => x.Email.StartsWith(Email));
            }

            if (Id != null)
            {
                query = query.Where(x => x.Id == Id);
            }

            query.ToList().ForEach(WriteObject);
        }

        private static AppUsers BuildOutputObject(ManagementBaseObject item)
        {
            return new AppUsers
            {
                Email = (string)item[nameof(AppUsers.Email)],
                Id = (int)item[nameof(AppUsers.Id)],
                UserName = (string)item[nameof(AppUsers.UserName)]
            };
        }


    }
}

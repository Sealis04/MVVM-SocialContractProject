using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class RemovePDFCommand : CommandBase
    {
        private readonly DatabaseQueries _dbQueries;
        private readonly NavigationService thisModel;

        public RemovePDFCommand(NavigationService thisModel, SocialContractMonitoringSystem scSystem)
        {
            _dbQueries = new DatabaseQueries();
            this.thisModel = thisModel;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;

namespace TfsMigrationUtility.Core.Configuration
{
    public class StringMigrationConfig : MigrationConfig
    {
        /// <summary>
        /// A string wrapper arround the TFSProject. It sets the property "SourceProjectCollection" with a valid TFS connection.
        /// The authentication happens through the Windows Credential Store.
        /// </summary>
        public string SourceURL
        {
            get
            {
                return base.SourceProjectCollection.Uri.ToString();
            }
            set
            {
                Uri result = null;
                if (Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)) //check if it is a valid URL
                {
                    base.SourceProjectCollection = new TfsTeamProjectCollection(result);
                }

            }
        }
        /// <summary>
        /// A string wrapper arround the TFSProject. It sets the property "TargetProjectCollection" with a valid TFS connection.
        /// The authentication happens through the Windows Credential Store.
        /// </summary>
        public string TargetURL
        {
            get
            {
                return base.TargetProjectCollection.Uri.ToString();
            }
            set
            {
                Uri result = null;
                if (Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result))
                {
                    base.TargetProjectCollection = new TfsTeamProjectCollection(result);
                }
            }
        }
    }
}

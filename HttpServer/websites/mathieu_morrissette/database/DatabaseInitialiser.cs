using HttpServer.databases;
using HttpServer.helpers;
using HttpServer.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.database
{
    public static class DatabaseInitialiser
    {
        public static void CreateDatabase(BaseDatabase database)
        {
           database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `users` (
                        `Id` int(11) NOT NULL AUTO_INCREMENT,
                        `Username` varchar(50) DEFAULT NULL,
                        `Password` varchar(150) NOT NULL,
                        PRIMARY KEY (`Id`)
                    )
                "
            );
        }        
    }
}

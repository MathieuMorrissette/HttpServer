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

            database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `posts` (
	                    `Id` INT(11) NOT NULL AUTO_INCREMENT,
	                    `UserId` INT(11) NULL DEFAULT NULL,
	                    `Date` DATETIME NULL DEFAULT NULL,
	                    `Data` TEXT NULL,
	                    PRIMARY KEY (`Id`)
                    )
                "
            );

            database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `friends` (
	                    `UserId` INT(11) NOT NULL,
	                    `FriendId` INT(11) NOT NULL,
	                    PRIMARY KEY (`UserId`, `FriendId`)
                    )
                "
            );

            database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `friend_requests` (
	                    `Id` INT(11) NOT NULL AUTO_INCREMENT,
	                    `UserId` INT(11) NOT NULL,
	                    `RequestedUserId` INT(11) NOT NULL,
	                    `Message` TEXT NULL,
	                    PRIMARY KEY (`Id`)
                    )
                "
            );
        }        
    }
}

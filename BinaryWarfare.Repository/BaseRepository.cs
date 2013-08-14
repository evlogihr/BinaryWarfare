using System;
using System.Data.Entity;
using System.Linq;
using BinaryWarfare.Data;
using BinaryWarfare.Data.Migrations;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public abstract class BaseRepository
    {
        protected const int Sha1CodeLength = 40;
        protected const int UserNumberLength = 4;

        protected static Random rand = new Random();
        //protected const string GameStatusOpen = "open";
        //protected const string GameStatusFull = "full";
        //protected const string GameStatusInProgress = "in-progress";
        //protected const string GameStatusFinished = "finished";

        //protected const string MessageStateUnread = "unread";
        //protected const string MessageStateRead = "read";

        //protected const string MessageTypeGameJoined = "game-joined";
        //protected const string MessageTypeGameStarted = "game-started";
        //protected const string MessageTypeGameFinished = "game-finished";
        //protected const string MessageTypeGuessMade = "guess-made";

        protected DbContext context;

        public BaseRepository()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BinaryWarfareContext, Configuration>());
        }

        //protected void ValidateUserNumber(int number)
        //{
        //    var numberString = number.ToString();
        //    if (numberString.Length != UserNumberLength || numberString.Any(digit => !char.IsDigit(digit) || digit == '0'))
        //    {
        //        throw new ServerErrorException("Invalid Number", "ERR_INV_NUM");
        //    }
        //    for (var i = 0; i < numberString.Length - 1; i++)
        //    {
        //        for (int j = i + 1; j < numberString.Length; j++)
        //        {
        //            if (numberString[i] == numberString[j])
        //            {
        //                throw new ServerErrorException("Invalid Number", "ERR_INV_NUM");
        //            }
        //        }
        //    }
        //}

        protected User GetUser(int userId, BinaryWarfareContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user", "ERR_INV_USR");
            }
            return user;
        }
    }
}
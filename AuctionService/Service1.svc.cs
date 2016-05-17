using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuctionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IRestAuction
    {
        public static List<Auction> auctions = new List<Auction>
        {
            new Auction(0, "blondynka", 150),
            new Auction(1, "brunetka", 130),
            new Auction(2, "ruda", 100),
        };
        public static int cid = 3;

        public void Add(string name, double startingprice)
        {
            auctions.Add(new Auction(cid++, name, startingprice));
        }

        public string Finish(string id)
        {
            return auctions.Find(a => a.ID == int.Parse(id)).Finish();
        }

        public Auction Get(string id)
        {
            return auctions.Find(a => a.ID == int.Parse(id));
        }

        public List<Auction> GetAll()
        {
            return auctions;
        }

        public int Remainings()
        {
            return auctions.Count(a => a.Available);
        }

        public string Remove(string id)
        {
            if (auctions.Find(a => a.ID == int.Parse(id)) == null)
                return "nie ma takiej aukcji";
            auctions.RemoveAll(a => a.ID == int.Parse(id));
            return "ok";
        }

        public string UpdateBy(string id, string user, double price)
        {
            return auctions.Find(a => a.ID == int.Parse(id)).Bid(user, price);
        }
    }
}

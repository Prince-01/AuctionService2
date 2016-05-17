using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuctionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRestAuction
    {
        [OperationContract]
        [WebGet(UriTemplate = "auctions/",
            ResponseFormat = WebMessageFormat.Json)]
        List<Auction> GetAll();

        [OperationContract]
        [WebGet(UriTemplate = "auctions/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Auction Get(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/bid",
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UpdateBy(string id, string user, double price);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/remove",
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json)]
        string Remove(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/add",
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void Add(string name, double startingprice);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/finish",
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json)]
        string Finish(string id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Auction
    {
        public Auction(int id, string n, double p)
        {
            ID = id;
            Name = n;
            Price = p;
            Available = true;
            Bidders = new List<string>();
        }

        public string Bid(string name, double nprice)
        {
            if (Price >= nprice)
                return "price is below current price.";
            if (!Available)
                return "not available";
            if (Bidders[Bidders.Count - 1].Equals(name))
                return "you cannot bid yourself";
            Price = nprice;
            Bidders.Add(name);
            return "ok";
        }
        public string Finish()
        {
            if (!Available)
                return "nie mozna zakonczyc niedostepnej aukcji";
            return "ok";
        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public bool Available { get; set; }
        [DataMember]
        public List<string> Bidders { get; set; }
    }
}

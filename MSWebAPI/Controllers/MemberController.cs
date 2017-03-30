using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MSWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MSWebAPI.Controllers
{
    public class MemberController : ApiController
    {
        /// <summary>
        /// 新增會員
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddMemberResponse Post(AddMemberRequest request)
        {
            //輸出結果
            var response = new AddMemberResponse();
            // 連接MongoDB伺服器
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            //取得資料庫(Database)和集合(collection)
            MongoDatabaseBase db = (MongoDatabaseBase)client.GetDatabase("ntut");
            //取得members集合(collection)
            var membersCollection = db.GetCollection<MembersCollection>("members");

            //查詢是否已存在資料
            var query = Builders<MembersCollection>.Filter.Eq(e => e.uid, request.uid);
            var doc = membersCollection.Find(query).ToListAsync().Result.FirstOrDefault();
            if (doc == null)
            {
                //新增資料
                membersCollection.InsertOne(
                    new MembersCollection()
                    {
                        _id = ObjectId.GenerateNewId(),
                        uid = request.uid,
                        name = request.name,
                        phone = request.phone
                    });
            }
            else
            {
                response.ok = false;
                response.errMsg = "編號:" + request.uid + "的會員已存在。";
            }


            return response;
        }
        /// <summary>
        /// 編輯會員
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public EditMemberResponse Put(EditMemberRequest request)
        {
            var response = new EditMemberResponse();

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            MongoDatabaseBase db = (MongoDatabaseBase)client.GetDatabase("ntut");
            var membersCollection = db.GetCollection<MembersCollection>("members");

            var query = Builders<MembersCollection>.Filter.Eq(e => e.uid, request.uid);
            var doc = membersCollection.Find(query).ToListAsync().Result.FirstOrDefault();
            if (doc != null)
            {
                var update = Builders<MembersCollection>.Update
                                    .Set("name", request.name)
                                    .Set("phone", request.phone);

                membersCollection.UpdateOne(query, update);
            }
            else
            {
                response.ok = false;
                response.errMsg = "編號:" + request.uid + "的會員不存在。";
            }

            return response;
        }
        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeleteMemberResponse Delete(string id)
        {
            var response = new DeleteMemberResponse();

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            MongoDatabaseBase db = (MongoDatabaseBase)client.GetDatabase("ntut");
            var membersCollection = db.GetCollection<MembersCollection>("members");

            var query = Builders<MembersCollection>.Filter.Eq(e => e.uid, id);
            var result = membersCollection.DeleteOne(query);
            if (result.DeletedCount == 0)
            {
                response.ok = false;
                response.errMsg = "編號:" + id + "的會員不存在。";
            }

            return response;
        }
        /// <summary>
        /// 取得全部會員
        /// </summary>
        /// <returns></returns>
        public GetMemberListResponse Get()
        {
            var response = new GetMemberListResponse();

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            MongoDatabaseBase db = (MongoDatabaseBase)client.GetDatabase("ntut");
            var membersCollection = db.GetCollection<MembersCollection>("members");

            //空的查詢式
            var query = new BsonDocument();
            //查詢並取得結果
            var cursor = membersCollection.Find(query).ToListAsync().Result;

            foreach (var doc in cursor)
            {
                response.list.Add(
                    new MemberInfo() { uid = doc.uid, name = doc.name, phone = doc.phone }
                );
            }

            return response;
        }
        /// <summary>
        /// 取得指定會員
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetMemberInfoResponse Get(string id) {
            var response = new GetMemberInfoResponse();

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            MongoDatabaseBase db = (MongoDatabaseBase)client.GetDatabase("ntut");
            var membersCollection = db.GetCollection<MembersCollection>("members");

            var query = Builders<MembersCollection>.Filter.Eq(e => e.uid, id);
            var doc = membersCollection.Find(query).ToListAsync().Result.FirstOrDefault();
            if (doc != null)
            {
                response.data.uid = doc.uid;
                response.data.name = doc.name;
                response.data.phone = doc.phone;
            }
            else
            {
                response.ok = false;
                response.errMsg = "編號:" + id + "的會員不存在。";
            }

            return response;
        }
    }
}

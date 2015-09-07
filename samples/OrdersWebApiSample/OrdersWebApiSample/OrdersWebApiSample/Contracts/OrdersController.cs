// Template: Base Controller (ApiControllerBase.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OrdersWebApiSample.OrdersXml.Models;

// Do not modify this file. This code was generated by RAML Web Api 2 Scaffolder

namespace OrdersWebApiSample.OrdersXml
{
    [RoutePrefix("orders")]
    public partial class OrdersController : ApiController
    {


        /// <summary>
		/// Create a new purchase order
		/// </summary>
		/// <param name="purchaseordertype"></param>
        [HttpPost]
        [Route("")]
        public virtual IHttpActionResult PostBase(Models.PurchaseOrderType purchaseordertype)
        {
            // Do not modify this code
            return  ((IOrdersController)this).Post(purchaseordertype);
        }

        /// <summary>
		/// gets already shipped orders
		/// </summary>
		/// <returns>PurchaseOrdersType</returns>
        [ResponseType(typeof(PurchaseOrdersType))]
        [HttpGet]
        [Route("shipped")]
        public virtual IHttpActionResult GetBase()
        {
            // Do not modify this code
            return  ((IOrdersController)this).Get();
        }

        /// <summary>
		/// gets not shipped orders
		/// </summary>
		/// <returns>PurchaseOrdersType</returns>
        [ResponseType(typeof(PurchaseOrdersType))]
        [HttpGet]
        [Route("notshipped")]
        public virtual IHttpActionResult GetNotshippedBase()
        {
            // Do not modify this code
            return  ((IOrdersController)this).GetNotshipped();
        }

        /// <summary>
		/// gets an order by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>PurchaseOrderType</returns>
        [ResponseType(typeof(PurchaseOrderType))]
        [HttpGet]
        [Route("{id}")]
        public virtual IHttpActionResult GetByIdBase([FromUri] string id)
        {
            // Do not modify this code
            return  ((IOrdersController)this).GetById(id);
        }

        /// <summary>
		/// updates an order
		/// </summary>
		/// <param name="purchaseordertype"></param>
		/// <param name="id"></param>
        [HttpPut]
        [Route("{id}")]
        public virtual IHttpActionResult PutBase(Models.PurchaseOrderType purchaseordertype,[FromUri] string id)
        {
            // Do not modify this code
            return  ((IOrdersController)this).Put(purchaseordertype,id);
        }

        /// <summary>
		/// marks order as shipped
		/// </summary>
		/// <param name="content"></param>
		/// <param name="id"></param>
        [HttpPost]
        [Route("{id}/ship")]
        public virtual IHttpActionResult PostShipBase([FromBody] string content,[FromUri] string id)
        {
            // Do not modify this code
            return  ((IOrdersController)this).PostShip(content,id);
        }
    }
}

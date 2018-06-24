using System.Collections.Generic;
using System.Globalization;
using EInfrastructure.Core.AliYun.Tbk.Dto;
using EInfrastructure.Core.AliYun.Tbk.Param;
using EInfrastructure.Core.HelpCommon.Serialization;
using Newtonsoft.Json;
using RestSharp;

namespace EInfrastructure.Core.AliYun.Tbk
{
    public class TbkOpenApi : OpenApi
    {
        public TbkOpenApi(string appKey, string appSecret) : base(appKey, appSecret)
        {
        }

        #region 根据淘口令获取响应信息

        /// <summary>
        /// 根据淘口令获取响应信息
        /// </summary>
        /// <param name="tbCode">淘口令</param>
        /// <returns></returns>
        public string TpwdQuery(string tbCode)
        {
            string response = base.GetResponse("taobao.wireless.share.tpwd.query", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("password_content", tbCode);
                    return para;
                });
            return response;
        }

        /// <summary>
        /// 根据淘口令获取响应信息
        /// </summary>
        /// <param name="tbCode"></param>
        /// <returns></returns>
        public NaughtyPasswordQueryDto TpwdQueryGet(string tbCode)
        {
            var response = TpwdQuery(tbCode);
            if (!response.Contains("error_response"))
            {
                return new JsonCommon().Deserialize<NaughtyPasswordQueryDto>(response);
            }
            else
            {
                return new NaughtyPasswordQueryDto();
            }
        }

        #endregion

        #region 根据链接生成淘口令

        /// <summary>
        /// 根据链接生成淘口令
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string TpwdCreate(string url, string text = "超值活动，惊喜活动多多")
        {
            string response = base.GetResponse("taobao.wireless.share.tpwd.create", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("tpwd_param", JsonConvert.SerializeObject(new
                    {
                        url = url,
                        text = text
                    }));
                    return para;
                });
            return response;
        }

        /// <summary>
        /// 根据链接生成淘口令
        /// </summary>
        /// <param name="url">链接</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string TpwdCreateString(string url, string text = "超值活动，惊喜活动多多")
        {
            var response = TpwdCreate(url, text);
            if (!string.IsNullOrEmpty(response))
            {
                if (response.Contains("model"))
                {
                    return new JsonCommon().Deserialize<dynamic>(response).model;
                }
            }

            return "";
        }

        #endregion

        #region 转换为推广链接

        /// <summary>
        /// 转换为推广链接
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Convert(ConvertToMarketingParam param)
        {
            string response = base.GetResponse("taobao.tbk.item.convert", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("fields", param.Fields);
                    para.Add("num_iids", param.NumIds);
                    para.Add("adzone_id", param.AdzoneId);
                    para.Add("platform", param.Platform + "");
                    para.Add("unid", param.Unid);
                    para.Add("dx", param.Dx);
                    return para;
                });
            return response;
        }

        #endregion

        #region 得到优惠券信息

        /// <summary>
        /// 得到优惠券信息
        /// </summary>
        /// <param name="goodsId">商品id</param>
        /// <param name="activityId">优惠券id</param>
        /// <returns></returns>
        public string CouponGet(string goodsId, string activityId)
        {
            string response = base.GetResponse("taobao.tbk.coupon.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("activity_id", activityId);
                    para.Add("item_id", goodsId);
                    return para;
                });
            return response;
        }

        #endregion

        #region 根据商品名称获取得到优惠券信息

        /// <summary>
        /// 根据商品名称获取得到优惠券信息
        /// </summary>
        /// <param name="adzoneId">推广位id</param>
        /// <param name="goodsName">商品名称</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public string CouponsGetByGoodsName(string adzoneId, string goodsName, int pageSize = 20, int pageIndex = 1)
        {
            string response = base.GetResponse("taobao.tbk.dg.item.coupon.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("adzone_id", adzoneId);
                    para.Add("platform", "1");
                    para.Add("page_size", pageSize.ToString());
                    para.Add("q", goodsName);
                    para.Add("page_no", pageIndex.ToString());
                    return para;
                });
            return response;
        }

        #endregion

        #region 淘宝客商品查询

        /// <summary>
        /// 淘宝客商品查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GoodsGet(GoodsGetParam param)
        {
            string response = base.GetResponse("taobao.tbk.item.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("fields", param.Fields);
                    para.Add("q", param.Query);
                    para.Add("cat", param.Cat);
                    para.Add("itemloc", param.Itemloc);
                    para.Add("sort", param.Sort);
                    para.Add("is_tmall", param.IsTmall.ToString());
                    para.Add("is_overseas", param.IsOverseas.ToString());
                    para.Add("start_price", param.StartPrice.ToString());
                    para.Add("end_price", param.EndPrice.ToString());
                    para.Add("start_tk_rate", param.StartTkRate.ToString());
                    para.Add("end_tk_rate", param.EndTkRate.ToString());
                    para.Add("platform", param.Platform.ToString());
                    para.Add("page_no", param.PageNo.ToString());
                    para.Add("page_size", param.PageSize.ToString());
                    return para;
                });
            return response;
        }

        #endregion

        #region 淘宝客商品详情(简版)

        /// <summary>
        /// 淘宝客商品详情(简版)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GoodsBaseGet(GoodsBaseGetParam param)
        {
            string response = base.GetResponse("taobao.tbk.item.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("num_iids", param.NumIids);
                    para.Add("platform", param.Platform.ToString());
                    para.Add("ip", param.Ip);
                    return para;
                });
            return response;
        }

        #endregion

        #region 淘宝客店铺查询

        /// <summary>
        /// 淘宝客店铺查询
        /// </summary>
        /// <param name="param">查询店铺参数</param>
        /// <returns></returns>
        public string ShopGet(ShopGetParam param)
        {
            string response = base.GetResponse("taobao.tbk.shop.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("fields", param.Fields);
                    para.Add("q", param.Query);
                    para.Add("sort", param.Sort);
                    para.Add("is_tmall", param.IsTmall.ToString());
                    para.Add("start_credit", param.StartCredit.ToString());
                    para.Add("end_credit", param.EndCredit.ToString());
                    para.Add("start_commission_rate", param.StartCommissionRate.ToString());
                    para.Add("end_commission_rate", param.EndCommissionRate.ToString());
                    para.Add("start_total_action", param.StartTotalAction.ToString());
                    para.Add("end_total_action", param.EndTotalAction.ToString());
                    para.Add("start_auction_count", param.StartAuctionCount.ToString());
                    para.Add("end_auction_count", param.EndAuctionCount.ToString());
                    para.Add("platform", param.Platform.ToString());
                    para.Add("page_no", param.PageNo.ToString());
                    para.Add("page_size", param.PageSize.ToString());
                    return para;
                });
            return response;
        }

        #endregion

        #region 淘宝客店铺关联推荐查询

        /// <summary>
        /// 淘宝客店铺关联推荐查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string ShopRelatedGet(ShopRelatedParam param)
        {
            string response = base.GetResponse("taobao.tbk.shop.recommend.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("fields", param.Fields);
                    para.Add("user_id", param.UserId.ToString());
                    para.Add("count", param.Count.ToString());
                    para.Add("platform", param.Platform.ToString());
                    return para;
                });
            return response;
        }

        #endregion

        #region 淘抢购api

        /// <summary>
        /// 淘抢购api
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string TqgGet(TqgGetParam param)
        {
            string response = base.GetResponse("taobao.tbk.ju.tqg.get", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("adzone_id", param.AdzoneId);
                    para.Add("fields", param.Fields);
                    para.Add("start_time", param.StartTime.ToString(CultureInfo.InvariantCulture));
                    para.Add("end_time", param.EndTime.ToString(CultureInfo.InvariantCulture));
                    para.Add("page_no", param.PageNo.ToString());
                    para.Add("page_size", param.PageSize.ToString());
                    return para;
                });
            return response;
        }

        #endregion

        #region 超级搜索

        /// <summary>
        /// 超级搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MaterialDto MaterialGet(MaterialGetParam param)
        {
            string response = base.GetResponse("taobao.tbk.dg.material.optional", Method.POST,
                (Dictionary<string, string> para) =>
                {
//                    para.Add("start_dsr", param.StartDsr.ToString());
                    para.Add("page_size", param.PageSize.ToString());
                    para.Add("page_no", param.PageNo.ToString());
//                    para.Add("platform", param.PlatForm.ToString());
//                    para.Add("end_tk_rate", param.EndTkRate.ToString());
//                    para.Add("start_tk_rate", param.StartTkRate.ToString());
//                    para.Add("end_price", param.EndPrice.ToString());
//                    para.Add("start_price", param.StartPrice.ToString());
//                    para.Add("is_overseas", param.IsOverSeas.ToString().ToLower());
//                    para.Add("is_tmall", param.IsTmal.ToString().ToLower());
//                    para.Add("sort", param.Sort.ToString());
//                    para.Add("itemloc", param.ItemLoc.ToString());
//                    para.Add("cat", param.Cat.ToString());
                    para.Add("q", param.Q.ToString());
//                    para.Add("has_coupon", param.HasCoupon.ToString().ToLower());
//                    para.Add("ip", param.Ip.ToString());
                    para.Add("adzone_id", param.AdzoneId.ToString());
//                    para.Add("need_free_shipment", param.NeedFreeShipment.ToString().ToLower());
//                    para.Add("need_prepay", param.NeedPrepay.ToString().ToLower());
//                    para.Add("include_pay_rate_30", param.IncludePayRate30.ToString().ToLower());
//                    para.Add("include_good_rate", param.IncludeGoodRate.ToString().ToLower());
//                    para.Add("include_rfd_rate", param.IncludeRfdRate.ToString().ToLower());
//                    para.Add("npx_level", param.NpxLevel.ToString());
                    return para;
                });
            if (!response.Contains("error_response"))
            {
                return new JsonCommon().Deserialize<MaterialDto>(response);
            }

            return null;
        }

        #endregion
    }
}
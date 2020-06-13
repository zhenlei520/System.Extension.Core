// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using EInfrastructure.Core.AliYun.Tbk.Param;
using EInfrastructure.Core.AliYun.Tbk.Respose;
using EInfrastructure.Core.AliYun.Tbk.Respose.Success;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using RestSharp;

namespace EInfrastructure.Core.AliYun.Tbk
{
    /// <summary>
    /// 淘宝客api
    /// </summary>
    public class TbkOpenApi : OpenApi
    {
        /// <summary>
        /// 淘宝客api
        /// </summary>
        /// <param name="appKey">appKey</param>
        /// <param name="appSecret">app秘钥</param>
        /// <param name="jsonProvider"></param>
        public TbkOpenApi(string appKey, string appSecret, IJsonProvider jsonProvider = null) : base(appKey,
            appSecret, jsonProvider)
        {
        }

        #region 根据淘口令获取响应信息

        #region 根据淘口令获取响应信息

        /// <summary>
        /// 根据淘口令获取响应信息
        /// </summary>
        /// <param name="tbCode">淘口令</param>
        /// <returns></returns>
        private string TpwdQuery(string tbCode)
        {
            string response = GetResponse("taobao.wireless.share.tpwd.query", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("password_content", tbCode);
                    return para;
                });
            return response;
        }

        #endregion

        /// <summary>
        /// 根据淘口令获取响应信息
        /// </summary>
        /// <param name="tbCode"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public NaughtyPasswordQueryDto TpwdQueryGet(string tbCode, int? errCode = null)
        {
            var response = TpwdQuery(tbCode);
            NaughtyPasswordQueryDto naughtyPasswordQuery = null;
            GetResult(response, (NaughtyPasswordQueryDto passwordQuery) => { naughtyPasswordQuery = passwordQuery; },
                (ErrDto err) =>
                {
                    throw new BusinessException(err.ErrorResponse.SubCode, errCode ?? HttpStatus.Err.Id);
                });
            return naughtyPasswordQuery;
        }

        #endregion

        #region 根据优惠券链接生成淘口令

        #region 根据优惠券链接生成淘口令

        /// <summary>
        /// 根据优惠券链接生成淘口令
        /// </summary>
        /// <param name="url">口令跳转目标页</param>
        /// <param name="text">口令弹框内容</param>
        /// <param name="logo">口令弹框logoURL</param>
        /// <param name="userId">生成口令的淘宝用户ID</param>
        /// <param name="ext">扩展字段JSON格式</param>
        /// <returns></returns>
        private string TpwdCreate(string url, string text = "超值活动，惊喜活动多多", string logo = "", string userId = "",
            string ext = "")
        {
            if (!url.Contains("https:"))
            {
                url = $"https:{url}";
            }

            string response = GetResponse("taobao.tbk.tpwd.create", Method.POST,
                (para) =>
                {
                    para.Add("url", url);
                    para.Add("text", text);
                    if (!string.IsNullOrEmpty(logo))
                    {
                        para.Add("logo", logo);
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        para.Add("user_id", userId);
                    }

                    if (!string.IsNullOrEmpty(ext))
                    {
                        para.Add("ext", ext);
                    }

                    return para;
                });
            return response;
        }

        #endregion

        /// <summary>
        /// 根据链接生成淘口令
        /// </summary>
        /// <param name="url">口令跳转目标页</param>
        /// <param name="text">口令弹框内容</param>
        /// <param name="logo">口令弹框logoURL</param>
        /// <param name="userId">生成口令的淘宝用户ID</param>
        /// <param name="ext">扩展字段JSON格式</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public string TpwdCreateString(string url, string text = "超值活动，惊喜活动多多", string logo = "", string userId = "",
            string ext = "", int? errCode = null)
        {
            var response = TpwdCreate(url, text, logo);

            TaobaoTbkTpwdCreateResponseDto tpwdCreateResponse = null;
            GetResult(response, (TaobaoTbkTpwdCreateResponseDto result) => { tpwdCreateResponse = result; },
                (ErrDto err) =>
                {
                    throw new BusinessException(err.ErrorResponse.SubCode, errCode ?? HttpStatus.Err.Id);
                });

            return tpwdCreateResponse.Data.Model;
        }

        #endregion

        #region 淘宝客文本淘口令

        /// <summary>
        /// 淘宝客文本淘口令
        /// </summary>
        /// <param name="url">口令跳转目标页</param>
        /// <param name="text">口令弹框内容</param>
        /// <param name="password">口令文本</param>
        /// <param name="logo">口令弹框logoURL</param>
        /// <param name="userId">生成口令的淘宝用户ID</param>
        /// <param name="ext">扩展字段JSON格式</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public string TpwdMixCreate(string url, string text, string password, string logo = "", string userId = "",
            string ext = "", int? errCode = null)
        {
            if (!url.Contains("https:"))
            {
                url = $"https:{url}";
            }


            string response = GetResponse("taobao.tbk.tpwd.mix.create", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("url", url);
                    para.Add("text", text);
                    para.Add("password", password);
                    if (!string.IsNullOrEmpty(logo))
                    {
                        para.Add("logo", logo);
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        para.Add("user_id", userId);
                    }

                    if (!string.IsNullOrEmpty(ext))
                    {
                        para.Add("ext", ext);
                    }

                    return para;
                });
            TaobaoTbkTpwdMixCreateResponseDto tpwdMixCreateResponse = null;
            GetResult(response,
                (TaobaoTbkTpwdMixCreateResponseDto result) => { tpwdMixCreateResponse = result; },
                (ErrDto err) =>
                {
                    if (err != null)
                    {
                        throw new BusinessException(err.ErrorResponse.SubCode, errCode ?? HttpStatus.Err.Id);
                    }

                    throw new BusinessException("生成淘口令失败", errCode ?? HttpStatus.Err.Id);
                });

            switch (tpwdMixCreateResponse.TaobaoTbkTpwdMixCreate.Data.Status)
            {
                case "1":
                    return tpwdMixCreateResponse.TaobaoTbkTpwdMixCreate.Data.Password;
                case "2":
                default:
                    throw new BusinessException("生成淘口令失败", errCode ?? HttpStatus.Err.Id);
                case "3":
                    throw new BusinessException("生成淘口令失败，文本不符合规范", errCode ?? HttpStatus.Err.Id);
            }
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
            string response = GetResponse("taobao.tbk.item.convert", Method.POST,
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
            string response = GetResponse("taobao.tbk.coupon.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.dg.item.coupon.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.item.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.item.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.shop.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.shop.recommend.get", Method.POST,
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
            string response = GetResponse("taobao.tbk.ju.tqg.get", Method.POST,
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
        /// <param name="action"></param>
        /// <returns></returns>
        public MaterialDto MaterialGet(MaterialGetParam param, Action<Dictionary<string, string>> action = null)
        {
            string response = GetResponse("taobao.tbk.dg.material.optional", Method.POST,
                (Dictionary<string, string> para) =>
                {
                    para.Add("page_size", param.PageSize.ToString());
                    para.Add("page_no", param.PageNo.ToString());
                    para.Add("q", param.Q.ToString());
                    para.Add("adzone_id", param.AdzoneId);
                    action?.Invoke(para);
                    return para;
                });
            if (!response.Contains("error_response"))
            {
                return _jsonProvider.Deserialize<MaterialDto>(response);
            }

            return null;
        }

        #endregion
    }
}

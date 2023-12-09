﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWebApi_v1.Models.DbContext;
using TestWebApi_v1.Models.TruyenTranh.MangaView;
using TestWebApi_v1.Models.ViewModel.MangaView;
using TestWebApi_v1.Repositories;
using TestWebApi_v1.Service;

namespace TestWebApi_v1.Controllers
{
    [Route("Services")]
    [ApiController]
    public class User_MangaController : Controller
    {
        private readonly IUser_Manga_Model _userMangaModel;
        private readonly IMapper _mapper;
        public User_MangaController(IUser_Manga_Model _Model, IMapper mapper)
        {
            _userMangaModel = _Model;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("TheoDoiTruyen")]
        public async Task<IActionResult> Follow([FromForm] string IdUser, [FromForm] string IdManga)
        {
            var result = await _userMangaModel.TheoDoiTruyen(IdUser, IdManga);
            if (result.Value == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Respone { Status = "Success", Message = result.Message });
            }
            return StatusCode(StatusCodes.Status403Forbidden,
                    new Respone { Status = "Failed", Message = result.Message });
        }
        [HttpDelete]
        [Route("HuyTheoDoi/{IdUser}/{IdManga}")]
        public IActionResult UnFollow(string IdUser, string IdManga)
        {
            var result = _userMangaModel.HuyTheoDoi(IdUser, IdManga);
            if (result.Value == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Respone { Status = "Success", Message = result.Message });
            }
            return StatusCode(StatusCodes.Status403Forbidden,
                    new Respone { Status = "Failed", Message = result.Message });
        }
        [HttpGet]
        [Route("DanhsachTheoDoi/{idUser}")]
        public async Task<List<MangaFollowing>> ListFollowing(string idUser)
        {
            var routeAttribute = ControllerContext.ActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(RouteAttribute), false).FirstOrDefault() as RouteAttribute;
            string routeController = (routeAttribute != null) ? routeAttribute.Template : "";
            string requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            var result = await _userMangaModel.DanhSachTheoDoi(idUser, requestUrl, routeController);
            //var data = _mapper.Map<List<BookmarkView>>(result);
            return result;
        }
        [HttpGet]
        [Route("DanhSachThongBao")]
        public async Task<IEnumerable<ThongbaoUser>> ListNotification(string idUser)
        {
            var result = await _userMangaModel.LayToanBoThongBao(idUser);
            return result;
        }
        [HttpGet]
        [Route("DanhSachThongBaoChuaXem/{idUser}")]
        public async Task<IEnumerable<ThongbaoUser>> ListNotificationUnSeen(string idUser)
        {
            var result = await _userMangaModel.LayThongBaoChuaDoc(idUser);
            return result;
        }
        [HttpPost]
        [Route("ThemThongBao")]
        public async Task<IActionResult> ThemThongbao(string idUser, string message)
        {
            var result = await _userMangaModel.TaoThongBao(idUser, message);
            if (result.Value == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Respone { Status = "Success", Message = result.Message });
            }
            return StatusCode(StatusCodes.Status403Forbidden,
                new Respone { Status = "Failed", Message = result.Message });
        }
        [HttpPost]
        //Khi người dùng nhấp vào thông báo ở web client thì sẽ gửi request đến đây để đánh dấu là đã xem
        [Route("SeenNotification")]
        public async Task<bool> seenNotificaton([FromForm] string idNotification)
        {
            var result = await _userMangaModel.DaXemThongBao(idNotification);
            if (result.Value == true)
            {
                return true;
            }
            return false;
        }
        [HttpGet]
        [Route("DanhSachTruyen")]
        public async Task<IEnumerable<botruyenView>> ListMangaOfUser(string idUser)
        {
            var result = await _userMangaModel.DanhSachTruyenUserTao(idUser);
            return result;
        }
        [HttpGet]
        [Route("DanhSachUserTheoRole")]
        public async Task<IEnumerable<User>> ListUserOfRole(string idUser)
        {
            var result = await _userMangaModel.DanhSachUserTheoRole(idUser);
            return result;
        }
        [HttpPost]
        [Route("CapNhatView")]
        public async Task<IActionResult> UpdateViewManga(string idBotruyen, string ViewCount)
        {
            var result = await _userMangaModel.DemViewBoTruyen(idBotruyen, ViewCount);
            if (result.Value == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new Respone { Status = "Success", Message = result.Message });
            }
            return StatusCode(StatusCodes.Status403Forbidden,
                new Respone { Status = "Failed", Message = result.Message });
        }

        //Danh sách bình luận theo chương
        [HttpGet]
        [Route("GetListComment/{ChapterId}")]
        public async Task<List<danhSachBinhLuan>> GetListComment(string ChapterId)
        {
            var result =await _userMangaModel.danhSachBinhLuanTheoChuuong(ChapterId);
            if (result != null)
            {
                return result;
            }
            return new List<danhSachBinhLuan>() { };
        }
        //Danh sách phản hồi bình luận
        [HttpGet]
        [Route("ListReply/{IdComment}")]
        public async Task<List<danhSachReplyBinhLuan>> GetListReply(string IdComment)
        {
            var result = await _userMangaModel.layDanhSachPhanHoi(IdComment);
            if (result != null)
            {
                return result;
            }
            return new List<danhSachReplyBinhLuan>() { };
        }
        //Bình luận
        [HttpPost]
        [Route("Comment")]
        public async Task<bool> CommentChapter([FromForm] string IdUser, [FromForm] string IdChapter, [FromForm] string CommentData)
        {
            var result = await _userMangaModel.BinhLuanChuongTruyen(IdUser, IdChapter, CommentData);
            return result;
        }
        // Phản hồi bình luận
        [HttpPost]
        [Route("ReplyComment")]
        public async Task<bool> ReplyCommentChapter([FromForm] string IdComment, [FromForm] string IdUserReply, [FromForm] string ReplyData)
        {
            var result = await _userMangaModel.ReplyBinhLuanChuong(IdComment, IdUserReply, ReplyData);
            return result;
        }
    }
}
﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using api.Models.Dtos;
using api.Models.Entities;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var post = await _postRepository.Get();
            
            return Ok();
        }


        [HttpGet("getPostById")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            return Ok(await _postRepository.GetPostById(id));
        }

        [HttpGet("getPostByUser")]
        public async Task<IActionResult> GetPostByUser([FromQuery] int id)
        {
            return Ok(await _postRepository.GetPostsByUser(id));
        }

        [HttpGet("getPostPerPage")]
        public async Task<IActionResult> GetPostPerPage([FromQuery] PostPerPage postPage)
        {
            if (postPage.Page == 0)
                postPage.Page = 1;

            if (postPage.Limit == 0)
                postPage.Limit = int.MaxValue;

            var skip = (postPage.Page) * postPage.Limit;

            return Ok(await _postRepository.GetPostPerPage(postPage.Page, postPage.Limit));
        }

        [HttpGet("getPostCount")]
        public  int GetPostCount()
        {
            return  _postRepository.GetPostCount();
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePost([FromBody] PostDTO postDTO)
        {
            Post post = new Post()
            {
                Id = postDTO.Id,
                UserId = postDTO.UserId,
                Titulo = postDTO.Titulo,
                Text = "",
                TextByte = Encoding.ASCII.GetBytes(postDTO.Text)
                //Encoding.ASCII.GetBytes(postViewModel.Text)
                ///reverter 
                ///string someString = Encoding.ASCII.GetString(bytes);
            };
            await _postRepository.CreatePost(post);

            postDTO.Text = Encoding.ASCII.GetString(post.TextByte);
            return Ok(post);
        }

        

    }
}

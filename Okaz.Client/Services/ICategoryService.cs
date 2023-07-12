using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.Client.Services;
  public interface ICategoryService
  {
	 Task<IEnumerable<CategoryDTO>> GetCategories();
   Task<CategoryDTO> GetCategoyById(int id);
  }
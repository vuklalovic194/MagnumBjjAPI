﻿using Magnum_API_web_application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface IRepository <T> where T : class
	{
		Task <List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeproperties = null, bool tracked = true);
		Task <T> GetByIdAsync(Expression<Func<T, bool>> filter = null, string includeproperties = null, bool tracked = true);
		Task CreateAsync(T entity);
		Task DeleteAsync(T entity);
		Task SaveAsync();
	}
}

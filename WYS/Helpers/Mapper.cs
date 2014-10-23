using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.DTOS;
using AutoMapper;
using log4net;
using WYS.BusinessLayer.BusinessObjects;

namespace WYS.Helpers
{
    public static class MapperApi
    {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(MapperApi));



        public static IUserBo ToUserBo(UserDto userDto)
        {
            IUserBo userBo = null;

            try
            {
                Mapper.CreateMap<UserDto, UserBo>();
                userBo = Mapper.Map<UserBo>(userDto);
            }
            catch (Exception exception)
            {
                Logger.Error("API LAYER: ERROR IN CLASS: MAPPERAPI, METHOD: ToUserBo =>> EXCEPTION MESSAGE: "+exception.Message);
                throw;
            }
            return userBo;
        }


        public static UserDto ToUserDto(UserBo userBo)
        {
            UserDto userDto = null;

            try
            {
                Mapper.CreateMap<UserBo, UserDto>();
                userDto = Mapper.Map<UserDto>(userBo);
            }
            catch (Exception exception)
            {
                Logger.Error("API LAYER: ERROR IN CLASS: MAPPERAPI, METHOD: ToUserDto =>> EXCEPTION MESSAGE: " + exception.Message);
                throw;
            }
            return userDto;
        }


        public static List<UserDto> ToUserDtos(List<IUserBo> userBos)
        {

            List<UserDto> userDtos = null;
            try
            {
                Mapper.CreateMap<UserBo, UserDto>();
                userDtos = Mapper.Map<List<UserDto>>(userBos);
            }
            catch (Exception exception)
            {
                Logger.Error("API LAYER: ERROR IN CLASS: MAPPERAPI, METHOD: ToListUserDto =>> EXCEPTION MESSAGE: " + exception.Message);
                throw;
            }

            return userDtos;
        }

    }
}
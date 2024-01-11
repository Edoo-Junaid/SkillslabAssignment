using SkillslabAssigment.DAL.Common;
using SkillslabAssigment.DAL.Interface;
using SkillslabAssignment.Common.DTO;
using SkillslabAssignment.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssigment.DAL.DAL
{
    public class RoleRepository : GenericRepository<Role, byte>, IRoleRepository
    {
        public RoleRepository(DbConnection connection) : base(connection)
        {
        }

        public async Task<Role> CreateRoleWithPageAndPageElementPermission(RolePagePermissionDTO rolePagePermission)
        {
            await _connection.OpenAsync();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    Role role = await _connection.ExecuteInsertQueryAsync<Role>(new Role { Name = rolePagePermission.RoleName }, transaction);
                    foreach (var webPage in rolePagePermission.WebPageElementPermissionDTO)
                    {
                        foreach (var uiComponentId in webPage.ListUiComponentId)
                        {
                            AccessRight accessRight = new AccessRight
                            {
                                RoleId = role.Id,
                                UiComponentId = uiComponentId,
                                WebPageId = webPage.PageId
                            };
                            await _connection.ExecuteInsertQueryAsync<AccessRight>(accessRight, transaction);


                        }
                    }
                    transaction.Commit();
                    return role;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public async Task<IEnumerable<RolePagePermissionDTO>> GetAllRolePagePermission()
        {
            List<RolePagePermissionDTO> rolePagePermissions = new List<RolePagePermissionDTO>();
            IEnumerable<Role> roles = await _connection.GetAllAsync<Role>();

            foreach (var role in roles)
            {
                var rolePagePermissionDTO = await MapRoleToDTO(role);
                rolePagePermissions.Add(rolePagePermissionDTO);
            }

            return rolePagePermissions;
        }

        private async Task<RolePagePermissionDTO> MapRoleToDTO(Role role)
        {
            var rolePagePermissionDTO = new RolePagePermissionDTO
            {
                RoleId = role.Id,
                RoleName = role.Name,
                WebPageElementPermissionDTO = new List<WebPageElementPermissionDTO>()
            };

            const string GET_ALL_PAGE_BY_ROLE_ID = @"
                SELECT web_page_id FROM access_right WHERE role_id=@RoleId GROUP BY web_page_id
            ";
            IEnumerable<AccessRight> webpages = await _connection.ExecuteQueryAsync<AccessRight>(GET_ALL_PAGE_BY_ROLE_ID, new
            {
                RoleId = role.Id
            });
            List<WebPageElementPermissionDTO> webPageElementPermissionDTOs = new List<WebPageElementPermissionDTO>();
            foreach (var webpage in webpages)
            {
                IEnumerable<AccessRight> accessRights = await _connection.SelectWhereAsync<AccessRight>("role_id= @RoleId AND web_page_id = @WebPageId", new
                {
                    RoleId = role.Id,
                    WebPageId = webpage.WebPageId
                });
                WebPageElementPermissionDTO web = new WebPageElementPermissionDTO
                {
                    PageId = webpage.WebPageId,
                    ListUiComponentId = accessRights.Select(x => x.UiComponentId).ToList(),
                };
                webPageElementPermissionDTOs.Add(web);
            }
            rolePagePermissionDTO.WebPageElementPermissionDTO = webPageElementPermissionDTOs;
            return rolePagePermissionDTO;
        }

        //private async Task<WebPageElementPermissionDTO> MapRolePagePermissionToDTO(RolePagePermission rolePagePermission)
        //{
        //    const string GET_WEBPAGE_ELEMENT_BY_ROLE_AND_WEBPAGE = @"
        //        SELECT DISTINCT [page_element].* FROM [role_page_permission]
        //        INNER JOIN [role] ON role_page_permission.role_id= [role].id
        //        INNER JOIN [role_page_element_permission] ON role_page_element_permission.role_id = [role].id
        //        INNER JOIN [page_element_relations] on page_element_relations.page_element_id = role_page_element_permission.page_element_id
        //        INNER JOIN [page_element] on page_element_relations.page_element_id = page_element.id
        //        WHERE [role].id = @RoleId AND [page_element_relations].web_page_id =@WebPageId
        //    ";
        //    IEnumerable<WebPageElement> webPageElements = await _connection.ExecuteQueryAsync<WebPageElement>(GET_WEBPAGE_ELEMENT_BY_ROLE_AND_WEBPAGE, new { RoleId = rolePagePermission.RoleId, WebPageid = rolePagePermission.WebPageId });
        //    var webPageElementPermissionDTO = new WebPageElementPermissionDTO
        //    {
        //        PageId = rolePagePermission.WebPageId,
        //        ListUiComponentId = webPageElements.Select(webPageElement => webPageElement.Id).ToList()
        //    };
        //    return webPageElementPermissionDTO;
        //}
        public async Task<IEnumerable<Role>> GetByUserIdAsync(short userId)
        {
            const string GET_BY_USER_ID_QUERY = @"
                SELECT role.* FROM user_role
                INNER JOIN [role] ON role.id = user_role.role_id
                WHERE user_role.user_id = @UserId
            ";
            return await _connection.ExecuteQueryAsync<Role>(GET_BY_USER_ID_QUERY, new { UserId = userId });
        }
    }
}

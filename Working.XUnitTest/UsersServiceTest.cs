/**********************
 * 
 * Moq ������
 * XUnit ���Կ��
 * 
 * ********************/

using Service;
using System;
using Xunit;
using Moq;
using IRepository;
using Model;

namespace Working.XUnitTest
{
    /// <summary>
    /// �û��������
    /// </summary>
    [Trait("�����", "UsersService")]
    public class UsersServiceTest
    {
        Mock<IDepartmentRepository> iDepartmentRepositoryMock = new Mock<IDepartmentRepository>();

        #region ��¼���� Login
        /// <summary>
        /// ���Ե�¼����ֵ
        /// </summary>
        [Fact]
        public void Login_Default_Return()//������_����ֵ_���
        {
            #region ģ��ִ���
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            
            #endregion

            #region ��֤��¼ҵ���߼��Ƿ���ȷ

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region ģ������ⲿ�����������ⲿ�����ķ�������

            //����Users���user��������
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = 1, UserName = "admin" };
            //ģ��LoginExtend���������Ƿ���user��������
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //����Roles���role��������
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //ģ��GetById����,���Ƿ���role��������
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region ����Login����

            var userRole = userServices.Login("admin", "admin");
            Assert.NotNull(userRole);//�����û���ɫ�ǿ�

            #endregion

            #endregion

        }

        /// <summary>
        /// ���Ե�¼�û������������
        /// </summary>
        [Fact]
        public void Login_NoUser_ThrowException()//������_���_���
        {
            #region ����ִ���
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region ��֤��¼ҵ���߼��Ƿ���ȷ

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region ģ������ⲿ�����������ⲿ�����ķ�������

            //����Users���user��������
            Users user = null;
            //ģ��LoginExtend���������Ƿ���user��������
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //����Roles���role��������
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //ģ��GetById����,���Ƿ���role��������
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region ����Login����

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });
            
            Assert.Contains("�û������������", exc.Message);//�����û���ɫ�ǿ�

            #endregion

            #endregion

        }

        /// <summary>
        /// ���Ե�¼ �û���ɫIDֵ����
        /// </summary>
        [Fact]
        public void Login_NoRoleID_ThrowException()//������_���_���
        {
            #region ����ִ���
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region ��֤��¼ҵ���߼��Ƿ���ȷ

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region ģ������ⲿ�����������ⲿ�����ķ�������

            //����Users���user��������
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = -1, UserName = "admin" };
            //ģ��LoginExtend���������Ƿ���user��������
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //����Roles���role��������
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //ģ��GetById����,���Ƿ���role��������
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region ����Login����

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });

            Assert.Contains("���û�û�н�ɫ", exc.Message);//�����û���ɫ�ǿ�

            #endregion

            #endregion

        }

        /// <summary>
        /// ���Ե�¼ û�н�ɫ����
        /// </summary>
        [Fact]
        public void Login_NoRole_ThrowException()//������_���_���
        {
            #region ����ִ���
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region ��֤��¼ҵ���߼��Ƿ���ȷ

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region ģ������ⲿ�����������ⲿ�����ķ�������

            //����Users���user��������
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = 1, UserName = "admin" };
            //ģ��LoginExtend���������Ƿ���user��������
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //����Roles���role��������
            Roles role = null;
            //ģ��GetById����,���Ƿ���role��������
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region ����Login����

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });

            Assert.Contains("���û��Ľ�ɫ������", exc.Message);//�����û���ɫ�ǿ�

            #endregion

            #endregion

        }

        #endregion
    }
}

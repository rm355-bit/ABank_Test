using ABankApi.Models;
using Dapper;
using Npgsql;

namespace ABankApi.Data
{
    public class UserRepository
    {

        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User?> GetUserByPhoneAndPassword(string telefono, string password)
        {
            var sql = "SELECT * FROM Users WHERE Telefono = @Telefono AND Password = @Password";
            using var connection = new NpgsqlConnection(_connectionString);
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new {Telefono = telefono, Password = password});
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var sql = "SELECT Id, Nombres, Apellidos, FechaNacimiento, Telefono, Email, Direccion From Users";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<UserDto>(sql);
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var sql = "SELECT Id, Nombres, Apellidos, FechaNacimiento, Telefono, Email, Direccion FROM Users WHERE Id = @Id";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { Id = id });
        }

        public async Task<UserDto> CreateUser(User user)
        {
            var sql = "INSERT INTO Users (Nombres, Apellidos, FechaNacimiento,Direccion, Password, Telefono, Email, Estado) VALUES (@Nombres, @Apellidos, @FechaNacimiento, @Direccion, @Password, @Telefono, @Email, 'A') RETURNING Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var newId = await connection.ExecuteScalarAsync<int>(sql, user);

            //return the user data
            var createdUser = await GetUserById(newId);
            return createdUser;
        }

        public async Task<bool> updateUser(int id, User user)
        {
            var sql = "UPDATE Users SET Nombres=@Nombres, Apellidos=@Apellidos,FechaNacimiento=@FechaNacimiento, Direccion=@Direccion, Password=@Password, Telefono=@Telefono, Email=@Email WHERE Id=@Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync(sql, new { id, user.Nombres, user.Apellidos, user.FechaNacimiento, user.Direccion, user.Password, user.Telefono, user.Email });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            using var connection = new NpgsqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

    }
}

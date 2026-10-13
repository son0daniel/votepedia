using elections.Commons.Constants;

namespace elections.Commons.Utils
{
    public static class RoleResolver
    {
        public static string? GetRole(int? role)
        {
            return role switch
            {
                1 => Constant.Election.Role.President,
                2 => Constant.Election.Role.Governor,
                3 => Constant.Election.Role.Mayor,
                4 => Constant.Election.Role.Senator,
                5 => Constant.Election.Role.HouseRepresentative,
                6 => Constant.Election.Role.StateRepresentative,
                7 => Constant.Election.Role.CityCouncilor,
                _ => null
            };
        }

        public static string GetValidRole(int? role)
        {
            var roleName = GetRole(role);

            if (string.IsNullOrEmpty(roleName))
            {
                throw new InvalidOperationException($"Invalid role: {role}");
            }

            return roleName;
        }

        public static string GetOfficeRole(string role, int levelNr)
        {
            if (string.Equals(role, Constant.Election.Role.President) || string.Equals(role, Constant.Election.Role.Governor) || string.Equals(role, Constant.Election.Role.Mayor))
            {
                return $"{(levelNr > 1 ? "VICE-" : string.Empty)}{role}";
            }

            if (string.Equals(role, Constant.Election.Role.Senator))
            {
                if (levelNr == 1)
                {
                    return role;
                }

                return $"{levelNr - 1}º SUPLENTE";
            }

            throw new InvalidOperationException($"Role must be one of majority office roles: {Constant.Election.Role.President}, {Constant.Election.Role.Governor}, {Constant.Election.Role.Mayor} or {Constant.Election.Role.Senator}.");
        }
    }
}

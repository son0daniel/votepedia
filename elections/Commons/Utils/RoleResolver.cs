using static elections.Commons.Constants.Constant.Election;

namespace elections.Commons.Utils
{
    public static class RoleResolver
    {
        public static string? GetRole(int? role)
        {
            return role switch
            {
                1 => Role.President,
                2 => Role.Governor,
                3 => Role.Mayor,
                4 => Role.Senator,
                5 => Role.HouseRepresentative,
                6 => Role.StateRepresentative,
                7 => Role.CityCouncilor,
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
            if (string.Equals(role, Role.President) || string.Equals(role, Role.Governor) || string.Equals(role, Role.Mayor))
            {
                return $"{(levelNr > 1 ? "VICE-" : string.Empty)}{role}";
            }

            if (string.Equals(role, Role.Senator))
            {
                if (levelNr == 1)
                {
                    return role;
                }

                return $"{levelNr - 1}º SUPLENTE";
            }

            throw new InvalidOperationException($"Role must be one of majority office roles: {Role.President}, {Role.Governor}, {Role.Mayor} or {Role.Senator}.");
        }
    }
}

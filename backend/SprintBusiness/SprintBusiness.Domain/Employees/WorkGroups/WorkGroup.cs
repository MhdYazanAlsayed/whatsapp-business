using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Users.Keys;

namespace SprintBusiness.Domain.Users.WorkGroups
{
    public class WorkGroup : Entity
    {
        private WorkGroup(string name)
        {
            Name = name;
        }

        public WorkGroupId Id { get; private set; } = null!;
        public string Name { get; private set; }
        public int EmployeesCount { get; private set; }
        public List<Employee> Employees { get; private set; } = new();
        public List<Conversation> Conversations { get; private set; } = new();

        public static WorkGroup Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return new WorkGroup(name);
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void AddEmployees(List<Employee> employees)
        {
            Employees.AddRange(employees);
            EmployeesCount += employees.Count;
        }

        public void RemoveEmployees(List<Employee> employees)
        {
            Employees.RemoveAll(x => employees.Contains(x));
            EmployeesCount -= employees.Count;
        }
    }
}

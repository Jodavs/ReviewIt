using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Conditions;
using ServerDTOs.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.EFEntities;
using BDSA.ReviewIt.Server.StorageLayer.Repositories.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.ServerDTOs;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Remotion.Linq.Clauses;

namespace BDSA.ReviewIt.Server.StorageLayer.Utilities {
    public static class DTOExtensions {

        public delegate IExclusionCondition<IValue> ConsCreator(IValue value);
        public static ConsCreator CreateCondition<T>(ValueComparator<T> comparator) where T : IValue {
            return v => (IExclusionCondition<IValue>) new ExclusionCondition<T>(comparator, v);
        }
        public static readonly Dictionary<ExclusionCondition, ConsCreator> ConditionCreators = new Dictionary<ExclusionCondition, ConsCreator>
        {
            {ExclusionCondition.BOOL, CreateCondition<BoolValue>(BoolConditions.Compare) },
            {ExclusionCondition.INT_EQ, CreateCondition<IntValue>(IntConditions.Equal) },
            {ExclusionCondition.INT_NE, CreateCondition<IntValue>(IntConditions.NotEqual) },
            {ExclusionCondition.INT_GT, CreateCondition<IntValue>(IntConditions.GreaterThan) },
            {ExclusionCondition.INT_GE, CreateCondition<IntValue>(IntConditions.GreaterThanOrEqual) },
            {ExclusionCondition.INT_LT, CreateCondition<IntValue>(IntConditions.LessThan)},
            {ExclusionCondition.INT_LE, CreateCondition<IntValue>(IntConditions.LessThanOrEqual) },
            {ExclusionCondition.STRING_EQ, CreateCondition<StringValue>(StringConditions.Equal) },
            {ExclusionCondition.STRING_IN, CreateCondition<StringValue>(StringConditions.IsContained) },
        };

        public static UserDTO ConvertToDTO(this User entity) {
            return new UserDTO {
                Id = entity.Id,
                Name = entity.Name,
                Password = entity.Password
            };
        }

        public static User ConvertToEntity(this UserDTO dto) {
            return new User {
                Id = dto.Id,
                Name = dto.Name,
                Password = dto.Password
            };
        }

        public static StudyDTO ConvertToDTO(this Study entity) {
            return new StudyDTO {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ActivePhase = entity.ActivePhase?.ConvertToDTO(),
                Phases = entity.Phases?.Select(e => e.ConvertToDTO()).ToList(),
                Publications = entity.Publications?.Select(e => e.ConvertToDTO()).ToList(),
                Users = entity.Participants?.Select(e => e.ConvertToDTO()).ToList(),
                ExclusionCriteria = entity.ExclusionCriteria?.Select(e => e.ConvertToDTO()).ToList(),
                ClassificationCriteria = entity.ClassificationCriteria?.Select(e => e.ConvertToDTO()).ToList()
            };
        }

        public static Study ConvertToEntity(this StudyDTO dto) {
            return new Study {
                Id = dto.Id,
                Name = dto.Name,
                Phases = dto.Phases?.Select(d => d.ConvertToEntity()).ToList(),
                Description = dto.Description,
                ActivePhase = dto.ActivePhase?.ConvertToEntity(),
                Participants = dto.Users?.Select(d => d.ConvertToEntity()).ToList(),
                Publications = dto.Publications?.Select(d => d.ConvertToEntity()).ToList()
            };
        }

        public static PhaseDTO ConvertToDTO(this Phase entity) {
            return new PhaseDTO {
                Id = entity.Id,
                ConflictManager = entity.ConflictManager?.ConvertToDTO(),
                DisplayFields = entity.DisplayFields?.Select(e => e.ConvertToDTO()).ToList(),
                Participants = entity.UserParticipants?.Select(e => e.ConvertToDTO()).ToList(),
                InputFields = entity.InputFields?.Select(e => e.ConvertToDTO()).ToList(),
                IsAutomatic = entity.Automatic,
                OverlapPercentage = entity.OverlapPercentage,
                Purpose = entity.Purpose,
                StudyId = entity.StudyId,
                TaskDelegations = entity.TaskDelegations?.Select(e => e.ConvertToDTO()).ToList()
            };
        }

        public static Phase ConvertToEntity(this PhaseDTO dto) {
            return new Phase {
                Id = dto.Id,
                StudyId = dto.Id,
                Automatic = dto.IsAutomatic,
                OverlapPercentage = dto.OverlapPercentage,
                UserParticipants = dto.Participants?.Select(d => d.ConvertToEntity()).ToList(),
                DisplayFields = dto.DisplayFields?.Select(d => d.ConvertToEntity()).ToList(),
                Purpose = dto.Purpose,
                ConflictManager = dto.ConflictManager?.ConvertToEntity(),
                InputFields = dto.InputFields?.Select(d => d.ConvertToEntity()).ToList(),
                ConflictManagerId = dto.ConflictManager?.Id ?? 0,
                TaskDelegations = dto.TaskDelegations?.Select(d => d.ConvertToEntity()).ToList()
            };
        }

        public static UserPhaseParticipantDTO ConvertToDTO(this UserPhaseParticipant entity) {
            return new UserPhaseParticipantDTO {
                Id = entity.Id,
                Phase = entity.Phase.ConvertToDTO(),
                User = entity.User.ConvertToDTO()
            };
        }

        public static UserPhaseParticipant ConvertToEntity(this UserPhaseParticipantDTO dto) {
            return new UserPhaseParticipant {
                Id = dto.Id,
                PhaseId = dto.Phase?.Id ?? 0,
                Phase = dto.Phase?.ConvertToEntity(),
                User = dto.User?.ConvertToEntity(),
                UserId = dto.User?.Id ?? 0
            };
        }

        public static ReviewTaskDTO ConvertToDTO(this ReviewTask entity) {
            return new ReviewTaskDTO {
                Id = entity.Id,
                User = entity.User?.ConvertToDTO(),
                Answers = entity.Answers?.Select(e => e.ConvertToDTO()).ToList(),
                TaskDelegationId = entity.TaskDelegationId,
                IsSubmitted = entity.IsSubmitted
            };
        }

        public static ReviewTask ConvertToEntity(this ReviewTaskDTO dto) {
            return new ReviewTask {
                Id = dto.Id,
                Answers = dto.Answers?.Select(e => e.ConvertToEntity()).ToList(),
                TaskDelegationId = dto.TaskDelegationId,
                User = dto.User?.ConvertToEntity(),
                UserId = dto.User?.Id ?? 0,
                IsSubmitted = dto.IsSubmitted
            };
        }

        public static TaskDelegationDTO ConvertToDTO(this TaskDelegation entity) {
            return new TaskDelegationDTO {
                Id = entity.Id,
                PhaseId = entity.PhaseId,
                Publication = entity.Publication.ConvertToDTO(),
                Tasks = entity.Tasks?.Select(e => e.ConvertToDTO()).ToList()
            };
        }

        public static TaskDelegation ConvertToEntity(this TaskDelegationDTO dto) {
            return new TaskDelegation {
                Id = dto.Id,
                PhaseId  = dto.PhaseId,
                Publication = dto.Publication?.ConvertToEntity(),
                PublicationId = dto.Publication?.Id ?? 0,
                Tasks = dto.Tasks?.Select(d => d.ConvertToEntity()).ToList()
            };
        }

        public static PublicationDTO ConvertToDTO(this Publication entity) {
            return new PublicationDTO {
                Id = entity.Id,
                Active = entity.Active,
                Data = entity.Data?.Select(e => e.ConvertToDTO()).ToList(),
                StudyId = entity.StudyId
            };
        }

        public static Publication ConvertToEntity(this PublicationDTO dto) {
            return new Publication {
                Id = dto.Id,
                Active = dto.Active,
                StudyId = dto.StudyId,
                Data = dto.Data?.Select(d => d.ConvertToEntity()).ToList()
            };
        }

        public static Data ConvertToEntity(this DataDTO dto) {
            return new Data {
                Id = dto.Id,
                Value = dto.Value?.GetString(),
                FieldId = dto.FieldId,
                PublicationId = dto.PublicationId
            };
        }

        public static DataDTO ConvertToDTO(this Data entity) {
            return new DataDTO {
                Id = entity.Id,
                PublicationId = entity.PublicationId,
                FieldId = entity.FieldId,
                Value = entity.Field != null ? CreateIValue(entity.Field.Type, entity.Value) : null
            };
        }

        public static ParticipantDTO ConvertToDTO(this Participant entity) {
            return new ParticipantDTO {
                Id = entity.Id,
                Role = entity.Role,
                StudyId = entity.StudyId,
                User = entity.User?.ConvertToDTO(),
            };
        }

        public static Participant ConvertToEntity(this ParticipantDTO dto) {
            return new Participant {
                Id = dto.Id,
                User = dto.User?.ConvertToEntity(),
                StudyId = dto.StudyId,
                Role = dto.Role,
                UserId = dto.User?.Id ?? 0
            };
        }

        public static FieldDTO ConvertToDTO(this Field entity) {
            return new FieldDTO {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type
            };
        }

        public static Field ConvertToEntity(this FieldDTO dto) {
            return new Field {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                Type = dto.Type
            };
        }

        public static ExclusionCriterionDTO ConvertToDTO(this ExclusionCriterion entity) {
            var op = entity.Field != null ? CreateIValue(entity.Field.Type, entity.Operator) : null;
            return new ExclusionCriterionDTO {
                Id = entity.Id,
                StudyId = entity.StudyId,
                FieldId = entity.FieldId,
                ExclusionCondition = entity.Condition,
                Condition = op != null ? ConditionCreators[entity.Condition](op) : null, // Fixed it with magic
                // WITCHCRAFT!! SLAY THE -- Ahem -- You are utterly amazing and always there when assistance is required. Have a magical day!
                // TODO Antonious assistance required to proceed beyond this point :(
            };
        }

        public static ExclusionCriterion ConvertToEntity(this ExclusionCriterionDTO dto) {
            return new ExclusionCriterion {
                Id = dto.Id,
                StudyId = dto.StudyId,
                Condition = dto.ExclusionCondition,
                FieldId = dto.FieldId,
                Operator = dto.Operator?.GetString()
            };
        }

        public static ClassificationCriterionDTO ConvertToDTO(this ClassificationCriterion entity) {
            return new ClassificationCriterionDTO {
                Id = entity.Id,
                Classifications = entity.Classifications?.Select(e => e.ConvertToDTO()).ToList(),
                Parent = entity.ParentCriterion?.ConvertToDTO(),
                StudyId = entity.StudyId,
            };
        }

        public static ClassificationCriterion ConvertToEntity(this ClassificationCriterionDTO dto) {
            return new ClassificationCriterion {
                Id = dto.Id,
                Classifications = dto.Classifications?.Select(d => d.ConvertToEntity()).ToList(),
                ParentCriterion = dto.Parent?.ConvertToEntity(),
                ParentCriterionId = dto.Parent?.Id ?? 0,
                StudyId = dto.StudyId,
            };
        }

        public static AnswerDTO ConvertToDTO(this Answer entity) {
            return new AnswerDTO {
                Id = entity.Id,
                Field = entity.Field?.ConvertToDTO(),
               // ReviewTask = entity.ReviewTask?.ConvertToDTO(),
                Value = entity.Field != null ? CreateIValue(entity.Field.Type, entity.Value) : null
            };
        }

        public static Answer ConvertToEntity(this AnswerDTO dto) {
            return new Answer {
                Id = dto.Id,
                FieldId = dto.Field?.Id ?? 0,
                ReviewTaskId = dto.ReviewTask?.Id ?? 0,
                Value = dto.Value?.ToString()
            };
        }

        /// <summary>
        /// Convert the string value in answer into either a string, int or boolean value
        /// </summary>
        /// <param name="type">The FieldType</param>
        /// <param name="value">The Value</param>
        /// <returns>The corresponding IValue</returns>
        private static IValue CreateIValue(FieldType type, string value) {
            switch (type) {
                case FieldType.BOOL:
                    return new BoolValue(bool.Parse(value));

                case FieldType.INT:
                    int intValue;
                    int.TryParse(value, out intValue);
                    return new IntValue(intValue);

                case FieldType.STRING:
                    return new StringValue(value);

                default:
                    throw new ArgumentException("The value in the answer was not a boolean, int or a string.");
            }
        }

        /// <summary>
        /// Taken from http://stackoverflow.com/questions/36369233/entity-framework-core-1-0-currentvalues-setvalues-does-not-exist
        /// Since the needed methods are not yet implemented in Entity Framework CORE :-(
        /// </summary>
        /// <typeparam name="TOrig"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="original"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static TOrig UpdateProperties<TOrig, TDTO>(this TOrig original, TDTO dto) {
            var origProps = typeof(TOrig).GetProperties();
            var dtoProps = typeof(TDTO).GetProperties();

            foreach (PropertyInfo dtoProp in dtoProps) {
                origProps
                    .Where(origProp => origProp.Name == dtoProp.Name)
                    .Single()
                    .SetMethod.Invoke(original, new Object[]
                        {
                    dtoProp.GetMethod.Invoke(dto, null) });
            }


            return original;
        }

        /*private static List<T> convertListToDTO<T>(this T entityList) where T : IEnumerable<T> {
            dynamic type = entityList.GetType();
            return entityList?.Select(e => e.ConvertToDTO())
        }*/
    }
}

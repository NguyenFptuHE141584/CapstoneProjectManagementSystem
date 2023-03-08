use [master]
go
-- if database have name is CapstoneProjectManagement exists , drop database
if EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'CapstoneProjectManagement')
drop database [CapstoneProjectManagement]
go
-- if database have name is CapstoneProjectManagement not exist ,create database
if NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'CapstoneProjectManagement')
create database [CapstoneProjectManagement]
go
use [CapstoneProjectManagement]
go 

-- create table user
create table [Users](
	[User_ID]			varchar(200)	not null		primary key,
	[UserName]			varchar(50)		not null,   -- ex: nguyennhhe141584,hieunxhe141543 ,lampt2
	FptEmail			varchar(100)	not null		unique,
	FullName			nvarchar(50)	not null,
	Avatar				varchar(255)	null,
	Gender				int				default(2),
	Role_ID				int				null,    --1:student, 2:supervisor, 3:staff, 4:devhead
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,
)

go
--create table BackupAccount
create table AffiliateAccounts(
	[AffiliateAccount_ID]	varchar(200)	not null		primary key,
	PersonalEmail			varchar(100)	not null		unique,
	PasswordHash			varchar(100)	null,
	One_Time_Password		varchar(6)		null,
	OTP_Request_Time		datetime		null,
	IsVerifyEmail			bit 			default(0),
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,
)

go 
-- create table staff
create table [Staffs](
	[Staff_ID]			varchar(200)	not null		primary key,
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go
-- create table student
create table [Students](
	Student_ID			varchar(200)	not null		primary key,
	RollNumber			varchar(8)		null,
	Curriculum			varchar(50)		null,
	SelfDiscription		nvarchar(3000)	null,
	ExpectedRoleInGroup nvarchar(50)	null,
	PhoneNumber			varchar(10)		null,
	LinkFacebook		varchar(200)	null,
	EmailAddress		varchar(200)	null,
	Semester_ID			int				null,
	FinalGroup_ID		int				null,
	Profession_ID		int				null,
	Specialty_ID		int				null,
	GroupName			varchar(100)	null,
	IsLeader			bit				default(0), --1:leader
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go
-- create table supervisor
create table [Supervisors](
	Supervisor_ID		varchar(200)	not null		primary key,
	IsDeavHead			bit				default(0), --1:devHead
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go
-- create table role 
create table [Roles](
	Role_ID				int				not null		primary key			identity(1,1),
	RoleName			varchar(50)		not null,
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null	
)

go
--create table permission
create table [Permissions](
	Permission_ID		int				not null		primary key			identity(1,1),
	PermissionName		varchar(50)		not null,
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go
--create table Role_Permission
create table [Role_Permission](
	Role_ID				int				not null,
	Permission_ID		int				not null,
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)
go 
--create table user guide
create table [UserGuides](
	UserGuide_ID		int				not null		primary key			identity(1,1),
	UserGuide_Link		varchar(200)	not null,
	Staff_ID			varchar(200)	not null,
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go 
--create table news
create table [News](
	News_ID				int				not null		primary key			identity(1,1),
	Title				nvarchar(300)	not null,
	Content				ntext			not null,
	Staff_ID			varchar(200)	not null,
	Pin					bit				default(0),
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,		
)

go
--create table support
create table [Supports](
	Support_ID			int				not null		primary key			identity(1,1),
	Student_ID			varchar(200)	not null,
	PhoneNumber			varchar(10)		null,
	ContactEmail		varchar(50)		null,
	SupportMessage		nvarchar(2000)	not null,
	Attachment			varchar(max)	null,  -- processing type
	Title				nvarchar(2000)	null,
	[Status]			int				default(0),  --1:responed
	Created_At			datetime		default(current_timestamp),
	Updated_At			datetime		null,
	Deleted_At			datetime		null,	
)
---------------------------------------
go

-- create table notification
create table [Notifications](
	[Notification_ID]		int				not null		primary key			identity(1,1),
	Readed					bit				default(0), --1:already read
	[User_ID]				varchar(200)	not null,
	Notification_Content	nvarchar(500)	not null,
	Attached_Link			varchar(200)	null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)

---------------------------
go
-- create table FinalGroup
create table [FinalGroups](
	FinalGroup_ID			int				not null			primary key					identity(1,1),
	GroupName				varchar(100)	null,
	[Description]			nvarchar(1000)	null,
	MaxMember				int				null,
	NumberOfMember          int				null,
	Profession_ID			int				not null,
	Specialty_ID			int				not null,
	ProjectEnglishName		varchar(100)	not null,
	ProjectVietNameseName	nvarchar(100)	not null,
	Abbreviation			varchar(50)		not null,
	Semester_ID				int				not null,
	Supervisor_ID			varchar(200)	null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,
)
go
-- create table change topic request
create table [ChangeTopicRequests](
	Request_ID				int				not null		primary key			identity(1,1),
	OldTopicNameEnglish		varchar(150)	not null,
	OldTopicNameVietNamese	nvarchar(150)   not null,
	OldAbbreviation			varchar(20)		null,
	NewTopicNameEnglish		varchar(150)	not null,
	NewTopicNameVietNamese	nvarchar(150)   not null,
	NewAbbreviation			varchar(20)		null,
	EmailSuperVisor			varchar(100)	not null,
	Reason_Change_Topic		nvarchar(500)	not null,
	FinalGroup_ID			int				not null,
	Staff_Comment			nvarchar(500)	null,
	[Status]				int				default(0),
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go
create table [Change_FinalGroup_Requests](
	Change_FinalGroup_Request_ID	int				not null		primary key			identity(1,1),
	FromStudent_ID					varchar(200)	not null,
	ToStudent_ID					varchar(200)	not null,
	StatusOfTo						int				default(0),
	[StatusOfStaff]					int				default(0),
	StaffComment					nvarchar(500)	null,
	Created_At						datetime		default(current_timestamp),
	Updated_At						datetime		null,
	Deleted_At						datetime		null,
)

go 
-- create table ReportMaterial
create table [ReportMaterials](
	Report_ID				int				not null		primary key			identity(1,1),
	Report_Tile				nvarchar(100)	not null,
	ReportContent			nvarchar(500)	null,
	[Status]				int				default(0),
	DueDate					datetime		not null,
	FinalGroup_ID			int				not null,
	Submission_Comment		nvarchar(500)	null,
	Submission_Attachment	varchar(max)    null,
	Supervisor_ID			varchar(200),
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go 
--  create table groupidea
create table [GroupIdeas](
	GroupIdea_ID			int				not null		primary key			identity(1,1),
	Profession_ID			int				not null,
	Specialty_ID			int				not null,
	ProjectEnglishName		varchar(100)	not null,
	ProjectVietNameseName	nvarchar(100)	not null,
	Abbreviation			varchar(20)		not null,   --viet tat
	[Description]			nvarchar(2000)	not null,
	ProjectTags				nvarchar(200)	not null,
	Semester_ID				int				not null,
	NumberOfMember			int				not null,
	MaxMember				int				not null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
-- create table manage member register of member of semester and professional
go
--create table [Student_groupIdea]
create table [Student_GroupIdea](
	Student_ID				varchar(200)	not null,
	GroupIdea_ID			int				not null,
	[Status]				int				default(1),
	[Message]				nvarchar(500)	null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)


go
-- create table Profession
create table [Professions](
	Profession_ID			int				not null			primary key				identity(1,1),
	Profession_Abbreviation varchar(50)		null,
	Profession_FullName		nvarchar(100)	not null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go
-- create table specialty
create table [Specialties](
	Specialty_ID			int				not null			primary key				identity(1,1),
	Specialty_Abbreviation	varchar(50)		null,
	Specialty_FullName		nvarchar(100)	not null,
	Profession_ID			int				not null,
	MaxMember				int				null,
	CodeOfGroupName			varchar(20)			null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go
-- create table semester
create table [Semesters](
	Semester_ID				int				not null			primary key				 identity(1,1),
	Semester_Name			varchar(50)		not null,
	Semester_Code			varchar(20)		not null,
	Start_Time				date			null,
	End_Time				date			null,
	StatusCloseBit			bit				default(1),
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go
-- create table defenceschedule
create table [DefenceSchedules](
	DefenceSchedule_ID		int				not null			primary key					identity(1,1),
	[Type]					int				null,
	[DateDefenceschedule]	date			null,
	[TimeDefenceschedule]	time			null,
	[RoomDefenceschedule]	varchar(30)		null,
	Concil_Infor			varchar(450)	null,
	FinalGroup_ID			int				not null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
)
go
create table [RegisteredGroups](
	RegisteredGroup_ID				int				not null		primary key						identity(1,1),
	GroupIdea_ID					int				not null,
	Registered_Supervisor_Name_1	nvarchar(100)	null,
	Registered_Supervisor_Name_2	nvarchar(100)	null,
	Registered_Supervisor_Phone_1	nvarchar(100)	null,
	Registered_Supervisor_Phone_2	nvarchar(100)	null,
	Registered_Supervisor_Email_1	nvarchar(100)	null,
	Registered_Supervisor_Email_2	nvarchar(100)	null,
	Student_Comment					nvarchar(150)	null,
	[Status]						int				default(0),
	Staff_Comment					nvarchar(400)	null,
	Students_Registraiton			varchar(500)	null,
	Created_At						datetime		default(current_timestamp),
	Updated_At						datetime		null,
	Deleted_At						datetime		null,
)
--------------------------------------------------------------------------------------------------
go
alter table [Change_FinalGroup_Requests]
add constraint FK_Change_FinalGroup_Requests_FromStudents foreign key ([FromStudent_ID]) references [Students]([Student_ID])

go
alter table [Change_FinalGroup_Requests]
add constraint FK_Change_FinalGroup_Requests_ToStudent foreign key ([ToStudent_ID]) references [Students]([Student_ID])

go
alter table [Notifications]
add constraint FK_Notifications_Users foreign key ([User_ID]) references [Users]([User_ID])

go
-- add relatation 1-1 Staff and User 
ALTER TABLE [Staffs]
ADD CONSTRAINT FK_Staffs_Users FOREIGN KEY([Staff_ID]) REFERENCES [Users]([User_ID])

go
-- add relatation 1-1  and User 
ALTER TABLE [Students]
ADD CONSTRAINT FK_Students_Users FOREIGN KEY([Student_ID]) REFERENCES [Users]([User_ID])

go
-- add relatation 1-1 Supervisor and User 
ALTER TABLE [Supervisors]
ADD CONSTRAINT FK_Supervisors_Users FOREIGN KEY([Supervisor_ID]) REFERENCES [Users]([User_ID])

go 
-- add foreign key role_id of table User with table Role 
ALTER TABLE [Users]
ADD CONSTRAINT FK_Users_Roles FOREIGN KEY([Role_ID]) REFERENCES [Roles]([Role_ID])

go
-- add foreign key role_id of table Role_Permission with table Role
ALTER TABLE [Role_Permission]
ADD CONSTRAINT FK_Role_Permission_Role FOREIGN KEY([Role_ID]) REFERENCES [Roles]([Role_ID])

go
-- add foreign key permission_id of table Role_Permission with table Permission
ALTER TABLE [Role_Permission]
ADD CONSTRAINT FK_Role_Permission_Permissions FOREIGN KEY([Permission_ID]) REFERENCES [Permissions]([Permission_ID])

go
-- add foreign key Staff_ID of table UserGuide with Staff
ALTER TABLE [UserGuides]
ADD CONSTRAINT FK_UserGuide_Staff FOREIGN KEY([Staff_ID]) REFERENCES [Staffs]([Staff_ID])

go
-- add foreign key Staff_ID of table News with Staff
ALTER TABLE [News]
ADD CONSTRAINT FK_News_Staffs FOREIGN KEY([Staff_ID]) REFERENCES [Staffs]([Staff_ID])

go
-- add foreign key Student_ID of table Support with Student
alter table [Supports]
add constraint FK_Supports_Students foreign key ([Student_ID]) references [Students]([Student_ID])

go
-- add foreign key Staff_ID of table Support with Staff
--alter table [Supports]
--add constraint FK_Staffs_Students foreign key ([Staff_ID]) references [Staffs]([Staff_ID])

go
-- add foreign key BackupAccount_ID of table BackupAccounts with table User
alter table [AffiliateAccounts]
add constraint FK_AffiliateAccounts_Users foreign key ([AffiliateAccount_ID]) references [Users]([User_ID])
------------------------------------------------------------------------------------------------

go
-- add foreign key GroupIdea_ID of table GroupIdea with RegisteredGroup
alter table [RegisteredGroups] 
add constraint FK_RegisteredGroups_GroupIdeas foreign key ([GroupIdea_ID]) references [GroupIdeas]([GroupIdea_ID])


go
-- add foreign key GroupIdea_ID of table Student_GroupIdea with table GroupIdea
alter table [Student_GroupIdea]
add constraint FK_Student_GroupIdea_GroupIdeas foreign key ([GroupIdea_ID]) references [GroupIdeas]([GroupIdea_ID])

go
-- add foreign key Student_ID of table Student_GroupIdea with table Student
alter table [Student_GroupIdea]
add constraint FK_Student_GroupIdea_Students foreign key ([Student_ID]) references [Students]([Student_ID])

go
-- add foreign key Profession_ID of table Specialty with table Profession
alter table [Specialties]
add constraint FK_Specialties_Professions foreign key ([Profession_ID]) references [Professions]([Profession_ID])

go
-- add foreign key Profession_ID of table GroupIdea with table Profession
alter table [GroupIdeas]
add constraint FK_GroupIdeas_Professions foreign key ([Profession_ID]) references [Professions]([Profession_ID])

go

-- add foreign key Specialty_ID of table GroupIdea with table Specialty
alter table [GroupIdeas]
add constraint FK_GroupIdeas_Specialties foreign key ([Specialty_ID]) references [Specialties]([Specialty_ID])

go

-- add foreign key Semester_ID of table GroupIdea with table Semester
alter table [GroupIdeas]
add constraint FK_GroupIdeas_Semesters foreign key ([Semester_ID]) references [Semesters]([Semester_ID])


go

-- add foreign key FinalGroup_ID of table DefenceSchedule with table FinalGroup
alter table [DefenceSchedules]
add constraint FK_DefenceSchedules_FinalGroups foreign key([FinalGroup_ID]) references [FinalGroups]([FinalGroup_ID])

go
-- add foreign key FinalGroup_ID of table ReportMaterial with table FinalGroup
alter table [ReportMaterials]
add constraint FK_ReportMaterials_FinalGroups foreign key([FinalGroup_ID]) references [FinalGroups]([FinalGroup_ID])

go
-- add foreign key Supervisor_ID of table ReportMaterial with table Supervisor
alter table [ReportMaterials]
add constraint FK_ReportMaterials_Supervisors foreign key([Supervisor_ID]) references [Supervisors]([Supervisor_ID])


go
-- add foreign key FinalGroup_ID of table ChangeTopicRequest with table FinalGroup
alter table [ChangeTopicRequests]
add constraint FK_ChangeTopicRequest_FinalGroup foreign key([FinalGroup_ID]) references [FinalGroups]([FinalGroup_ID])

go
-- add foreign key FinalGroup_ID of table Student with table FinalGroup
alter table [Students]
add constraint FK_Students_FinalGroups foreign key([FinalGroup_ID]) references [FinalGroups]([FinalGroup_ID])

go
-- add foreign key Profession_ID of table FinalGroup with table Profession
alter table [FinalGroups]
add constraint FK_FinalGroups_Professions foreign key([Profession_ID]) references [Professions]([Profession_ID])

go
-- add foreign key Specialty_ID of table FinalGroup with table Specialty
alter table [FinalGroups]
add constraint FK_FinalGroups_Specialties foreign key([Specialty_ID]) references [Specialties]([Specialty_ID])

go
-- add foreign key Semester_ID of table FinalGroup with table Semester
alter table [FinalGroups]
add constraint FK_FinalGroups_Semesters foreign key([Semester_ID]) references [Semesters]([Semester_ID])

go
-- add foreign key Supervisor_ID of table FinalGroup with table Supervisor
alter table [FinalGroups]
add constraint FK_FinalGroups_Supervisors foreign key([Supervisor_ID]) references [Supervisors]([Supervisor_ID])

go
alter table [Students]
add constraint FK_Professions_Students foreign key ([Profession_ID]) references[Professions]([Profession_ID])

go
alter table [Students]
add constraint FK_Specialties_Students foreign key ([Specialty_ID]) references[Specialties]([Specialty_ID])

go
alter table [Students]
add constraint FK_Semesters_Students foreign key([Semester_ID]) references [Semesters]([Semester_ID])

go
--add attribute for semester 
alter table Semesters
add showGroupName bit default(0)
go
/*profession and specialty*/
alter table Professions
add Semester_ID int ;
go
alter table [Professions]
add constraint FK_Professions_Semesters foreign key ([Semester_ID]) references [Semesters]([Semester_ID]);
go
alter table Specialties
add Semester_ID int ;
go
alter table [Specialties]
add constraint FK_Specialties_Semesters foreign key ([Semester_ID]) references [Semesters]([Semester_ID]);
go
/*Student_FavoriteGroupIdea*/
create table [Student_FavoriteGroupIdea](
	Student_ID				varchar(200)	not null,
	GroupIdea_ID			int				not null,
	Created_At				datetime		default(current_timestamp),
	Updated_At				datetime		null,
	Deleted_At				datetime		null,	
);
go
alter table [Student_FavoriteGroupIdea]
add constraint FK_Student_FavoriteGroupIdea_GroupIdeas foreign key ([GroupIdea_ID]) references [GroupIdeas]([GroupIdea_ID]);
go
alter table [Student_FavoriteGroupIdea]
add constraint FK_Student_FavoriteGroupIdea_Students foreign key ([Student_ID]) references [Students]([Student_ID]);
go 
alter table AffiliateAccounts
ALTER COLUMN PersonalEmail varchar(100) null;

-------------------------------------- note ------------------------------------------
go 
alter table RegisteredGroups
alter column Registered_Supervisor_Phone_1 nvarchar(10) null

go 
alter table RegisteredGroups
alter column Registered_Supervisor_Phone_1 nvarchar(10) null


go
alter table ChangeTopicRequests
alter column Reason_Change_Topic nvarchar(3000) not null

go 
alter table ChangeTopicRequests
alter column OldTopicNameEnglish varchar(100) not null

go 
alter table ChangeTopicRequests
alter column OldTopicNameVietNamese nvarchar(100) not null

go 
alter table ChangeTopicRequests
alter column NewTopicNameEnglish varchar(100) not null

go 
alter table ChangeTopicRequests
alter column NewTopicNameVietNamese nvarchar(100) not null
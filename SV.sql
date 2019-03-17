CREATE DATABASE SinhVien
USE SinhVien

GO
Create table Khoa
(
	MaKhoa char(2) primary key,
	TenKhoa nvarchar(30) not null,
)
Go
Create table DMSinhVien(
	MaSV char(3)not null primary key,
	HoTenSV nvarchar(50)not null,
	Phai nchar(7),
	NgaySinh datetime not null,
	NoiSinh nvarchar(20),
	MaKhoa char(2),

	foreign key (MaKhoa) references Khoa(MaKhoa)
)
GO

Insert into Khoa(MaKhoa,TenKhoa)
values('AV',N'Anh Văn')
Insert into KHoa(MaKhoa,TenKhoa)
values('TH',N'Tin Học')
Insert into Khoa(MaKhoa,TenKhoa)
values('TR',N'Triết')
Insert into Khoa(MaKhoa,TenKhoa)
values('VL',N'Vật Lý')

GO
drop table DMSinhVien

Insert into DMSinhVien(MaSV,HoTenSV,Phai,NgaySinh,NoiSinh,MaKhoa)
values('B01',N'Trần Thanh Mai',N'Nữ','12/08/1991',N'Hải Phòng','TR')

Insert into DMSinhVien(MaSV,HoTenSV,Phai,NgaySinh,NoiSinh,MaKhoa)
values('B02',N'Trần Thị Thu Thủy', N'Nữ','02/01/1991',N'TP Hồ Chí Minh','AV')

select * from DMSinhVien
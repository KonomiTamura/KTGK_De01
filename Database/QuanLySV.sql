create database QuanLySV

use QuanLySV
create table Lop
(
	MaLop char(3) not null,
	TenLop nvarchar(30) not null
)

create table Sinhvien
(
	MaSV char(6) not null,
	HotenSV nvarchar(40) not null,
	MaLop char(3)
)
--Tao khoa chinh
alter table Lop Add constraint PK_MaLop primary key (Malop)
alter table Sinhvien Add constraint PK_MaSV primary key (MaSV)
--Tao khoa phu
ALTER TABLE Sinhvien ADD CONSTRAINT FK_Sinhvien_Lop FOREIGN KEY(Malop) REFERENCES Lop(MaLop)

--Table Lop--
insert into Lop values ('L01', 'Sun')
insert into Lop values ('L02', 'Moon')

--Table Sinhvien--
insert into Sinhvien values ('123456', 'Nguyen Van A', 'L01')
insert into Sinhvien values ('234567', 'Tran Thi B', 'L02')
insert into Sinhvien values ('345678', 'Luong Huu C', 'L01')
insert into Sinhvien values ('456789', 'Pham Gia D', 'L02')
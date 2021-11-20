
CREATE DATABASE CuaHangDienThoai
use CuaHangDienThoai

select * from chitiethdban

CREATE TABLE NHANVIEN(
	MaNV nvarchar(50) NOT NULL ,
	TenNV nvarchar(50) NULL,
	DiaChiNV nvarchar(50) NULL,
	SDT varchar(13) NULL,
	GioiTinh nvarchar(50) NULL,
	NgaySinh smalldatetime NULL,
    primary key (MaNV)
)


INSERT into NhanVien  VALUES (N'NV0001', N'Trần Nhơn Hòa', N'An Giang', N'0912605156', N'Nam', '06-08-1991')
INSERT into NhanVien  VALUES (N'NV0002', N'Nguyễn Thanh Hùng', N'Quảng Ngãi', N'0949567842', N'Nam ', '10-08-1990')
INSERT into NhanVien  VALUES (N'NV0004', N'Huỳnh Văn Huy', N'Kiên Giang', N'0933436721', N'Nam ', '06-08-1999')


CREATE TABLE SANPHAM(
	MaSP nvarchar(50) NOT NULL,
	MaNhom nvarchar(50) NULL,
	TenSP nvarchar(50) NULL,
	DonViTinh nvarchar(50) NULL,
	SLTon int NULL,
	GiaBan int NULL,
	GiaNhap int NULL,
	MaNCC nvarchar(50) NULL,
	primary key (MaSP)
)

INSERT into SANPHAM VALUES (N'SP001', N'MN004', N'SamSung J7 pro', N'Cái', 50, 7000000, 5000000, N'NCC0001')
INSERT into SANPHAM  VALUES (N'SP002', N'MN001', N'tai nghe bluetooth ', N'Cái', 20, 400000, 200000 ,  N'NCC0001')
INSERT into SANPHAM VALUES (N'SP003', N'MN004', N'SamSung Galaxy J7 Prime', N'Cái', 40 , 6000000 , 5000000 , N'NCC0001')
INSERT into SANPHAM  VALUES (N'SP004', N'MN001', N'sạt dự phòng SamSung ', N'Cái', 80, 600000, 370000, N'NCC0002')
INSERT into SANPHAM  VALUES (N'SP005', N'MN004', N'Oppo F5', N'Cái', 90, 7000000, 5800000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP006', N'MN004', N'Huawei Nova 2i', N'Cái', 70, 6000000, 3500000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP007', N'MN004', N'Iphone X 64GB', N'Cái', 120, 30000000, 27000000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP008', N'MN001', N'thẻ nhớ 8gb ', N'Cái', 160, 180000, 100000, N'NCC0001')
INSERT into SANPHAM VALUES (N'SP009', N'MN001', N'gậy tự sướng ', N'Cái', 80, 110000, 100000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP010', N'MN004', N'SamSung Galaxy S8 plus', N'Cái', 200, 20500000, 20000000,N'NCC0001')
INSERT into SANPHAM VALUES (N'SP011', N'MN001', N'ốp lưng SamSung j7 pro', N'Cái', 40, 90000, 50000, NULL)
INSERT into SANPHAM VALUES (N'SP012', N'MN004', N'Oppo F5 Youth', N'Cái', 20, 6000000, 5500000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP013', N'MN001', N'sạt Oppo', N'Cái', 90, 80000, 70000, N'NCC0001')
INSERT into SANPHAM VALUES (N'SP014', N'MN001', N'Sạt Iphone', N'Cái', 10, 90000, 80000,N'NCC0001')
INSERT into SANPHAM VALUES (N'SP015', N'MN004', N'Oppo F3 plus', N'Cái',10, 11000000, 10000000, N'NCC0001')
INSERT into SANPHAM VALUES (N'SP016', N'MN004', N'Iphone 7 plus', N'Cái', 5 , 23000000, 20000000, N'NCC0002')
INSERT into SANPHAM VALUES (N'SP017', N'MN001', N'tai nghe Iphone', N'Cái', 60, 150000, 90000,  N'NCC0002')



/* OKE */

CREATE TABLE NHOMSANPHAM(
	MaNhom nvarchar(50) NOT NULL,
	TenNhom nvarchar(50) NULL,
	primary key(MaNhom)
)

INSERT into NHOMSANPHAM  VALUES (N'MN001', N'Phụ kiện')
INSERT into NHOMSANPHAM  VALUES (N'MN004', N'Điện thoại')






/*OKE duoc roi */



CREATE TABLE NHACUNGCAP(
	MaNCC nvarchar(50) NOT NULL,
	TenNCC nvarchar(50) NULL,
	SDTNCC varchar(13) NULL,
	DiaChiNCC nvarchar(50) NULL,
	primary key(MaNCC)
)
 
INSERT NHACUNGCAP VALUES (N'NCC0001', N'Thế giới di động', N'0756373658', N'Hà Nội')
INSERT NHACUNGCAP VALUES (N'NCC0002', N'Điện Máy Xanh', N'0744849176', N'TP.HCM')
INSERT NHACUNGCAP VALUES (N'NCC0003', N'FPT', N'0733123764', N'Đà Nẵng')


/*Oke dc */
CREATE TABLE HOADONBAN(
	MaHD nvarchar(50) NOT NULL,
	MaNV nvarchar(50) NULL,
	MaKH nvarchar(50) NULL,
	NgayBan smalldatetime NULL,
	TongTienBan int NULL,
	primary key(MaHD)
)
/* oke dc*/

CREATE TABLE CHITIETHDBAN(
	MaHD nvarchar(50) NOT NULL,
	MaSP nvarchar(50) NOT NULL,
	SLBan int NULL,
	DonGiaBan int NULL,
	ThanhTienBan int NULL,
	primary key(MaHD, MASP)
)
/* oke dc */
CREATE TABLE KHACHHANG(
	MaKH nvarchar(50) NOT NULL,
	TenKH nvarchar(50) NULL,
	DiaChiKH nvarchar(50) NULL,
	SDTKH varchar(12) NULL,
	primary key(MaKH)
)
create trigger Buy_Product on CHITiethdban for insert
as
update SANPHAM set SanPham.SLTon = SANPHAM.SLTon - inserted.SLBan from inserted
where SANPHAM.MaSP = inserted.MaSP


create trigger Return_Product on CHITiethdban for delete
as
update SANPHAM set SanPham.SLTon = SANPHAM.SLTon + deleted.SLBan from deleted
where SANPHAM.MaSP = deleted.MaSP


create trigger update_Product on Chitiethdban for update
as
begin
	update SANPHAM set SanPham.SLTon = SANPHAM.SLTon - inserted.SLBan from inserted
	where SANPHAM.MaSP = inserted.MaSP 
	update SANPHAM set SanPham.SLTon = SANPHAM.SLTon + deleted.SLBan from deleted
	where SANPHAM.MaSP = deleted.MaSP
end

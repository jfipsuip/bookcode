Program Main
Use Coordinate
    Implicit none
    Integer :: i=1
    Integer :: num
    type(Coor) :: Coor_0(6)
    type(Geo) :: Geo_0(6)
    type(Topo) :: Topo_0(6)
    !��ȡ�ļ�
    open(1,file="XYZ.dat")
    read(1,"(I1)") num
    Do i=1,num
        read(1,101) Coor_0(i).NAME,Coor_0(i).X,Coor_0(i).Y,Coor_0(i).Z
        call Coordinate2Geodetic(Coor_0(i),Geo_0(i))
    End Do
    !�������
    Do i=2,num
        call Coordinate2CopoCentric(Coor_0(1),Geo_0(1),Coor_0(i),Geo_0(i),Topo_0(i))
    End Do
    !������
    open(2,file="NEU.DAT")
    write(2,*)"----------���-----------"
    write(2,*)"�ļ������������",num
    write(2,*)"�Ե�һ����Ϊվ�ĵ�õ���վ��ֱ�����꣺"
    Do i=2,num
        write(2,102)Coor_0(1).Name,"-",Coor_0(i).Name,Topo_0(i).N,Topo_0(i).E,Topo_0(i).U
    End Do
    close(2)
    close(1)
    101format(A4,1X,3F13.4)
    102format(A4,A1,A4,3F13.4)
End Program Main
!Program Test
!    
!End Program Test
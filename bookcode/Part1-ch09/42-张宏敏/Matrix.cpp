#include<stdio.h>

//�������
void Multiply(int m1,int n1,int m2,int n2,double A[],double B[],double* C )
{
	double tmp=0;
	if(n1!=m2)
	{
		printf("�þ��������ڳ˻�����");
		return;
	}
	for(int i=0;i<m1;i++)
	{
		for(int j=0;j<n2;j++)
		{
			tmp=0;
			for(int k=0;k<n1;k++)
			{
				tmp+=A[i*n1+k]*B[k*n2+j];
			}
			C[i*n2+j]=tmp;
		}
	}

}
//宏定义    
#define  EXPORTBUILD    

//加载头文件    
#include "TestDLL.h"    

//设置函数    
int _DLLExport MyADD(int x, int y)
{
	return x + y;
}
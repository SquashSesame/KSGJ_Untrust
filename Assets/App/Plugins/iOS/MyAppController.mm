#import "UnityAppController.h"

@interface MyAppController : UnityAppController
@end

@implementation MyAppController
- (void) applicationDidReceiveMemoryWarning:(UIApplication*)application
{
    [super applicationDidReceiveMemoryWarning:application];
    UnitySendMessage("GameMain", "DidReceiveMemoryWarning","from iOS");
}
@end

IMPL_APP_CONTROLLER_SUBCLASS(MyAppController)

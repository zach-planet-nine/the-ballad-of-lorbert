#import "DeepLinkAppDelegate.h"

// Makes sure your app controller delegate is the one that gets loaded.
IMPL_APP_CONTROLLER_SUBCLASS(DeepLinkAppDelegate)

@implementation DeepLinkAppDelegate

- (void) deepLinkIsAlive
{
	if (_lastURL)
	{
		const char *URLString = [_lastURL cStringUsingEncoding:NSASCIIStringEncoding];
    	UnitySendMessage("URLOpener", "URLOpened", URLString);
	}
}
- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options {
    NSLog(@"Is this being called now?");
    _lastURL = url.absoluteString;
    const char *URLString = [url.absoluteString UTF8String];
    UnitySendMessage("URLOpener", "URLOpened", URLString);

    return [super application:app openURL:url options:options];
}
- (char *) deepLinkURL
{
	return [(_lastURL ? _lastURL : @"") UTF8String];
}
@end

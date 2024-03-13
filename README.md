# ImageDownsizer

ImageDownsizer is a simple Windows Forms application that allows users to downsize images.

|         |10%      | 50%      | 80%|
|--|--|--|--|
Sequential| 3845ms | 18041ms | 30331ma|
Parallel  | 142ms | 2378ms | 11560ms|

Times masured from the application when testing with AG1121.jpg... As expected the parallel downsizing is quicker.

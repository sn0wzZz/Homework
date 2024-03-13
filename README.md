# ImageDownsizer

ImageDownsizer is a simple Windows Forms application that allows users to downsize images.

|         |10%      | 50%      | 80%|
|--|--|--|--|
Sequential| 3 845ms | 18 041ms | 30 331ma|
Parallel  | 142ms | 2 378ms | 11 560ms|

Times masured from the application when testing with AG1121.jpg... As expected the parallel downsizing is quicker.

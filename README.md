# ImageDownsizer

ImageDownsizer is a simple Windows Forms application that allows users to downsize images.

|         |20%      | 50%      |
|--|--|--|
Sequential| 48286ms | 163600ms |
Parallel  | 83325ms | 330535ms |

Times masured from the application when testing with AG1121.jpg... As expected the parallel downsizing is quicker.

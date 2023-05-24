from sys import argv
from PIL import Image

DEFAULT_OUTLINE_COLOR = (255,255,255,255)

def colorPixels(image:Image,color:tuple, pixelsToColor:tuple):
    for pixelPos in pixelsToColor:
        image.putpixel(pixelPos,color)

class ImageData:
    def __init__(self,image: Image)->None:
        self.image:Image = image
        self.width = self.image.size[0]
        self.height = self.image.size[1]
        self.pixels = self.image.load()

    
    def getSurroundingPixels(self,pixelPos) -> tuple:
        """Returns every surrounding pixel. If pixelPos is for example
        on the right edge of the picture, it won't return the pixel to the right"""
        result = []
        if pixelPos[0] > 0:
            result.append((pixelPos[0]-1,pixelPos[1]))
        if pixelPos[0] < self.width-1:
            result.append((pixelPos[0]+1,pixelPos[1]))
        if pixelPos[1] > 0:
            result.append((pixelPos[0],pixelPos[1]-1))
        if pixelPos[1] < self.height-1:
            result.append((pixelPos[0],pixelPos[1]+1))
        return tuple(result)
    def isTransparent(self,pixelPos):
        return self.pixels[pixelPos[0],pixelPos[1]][3] == 0
    

class ImageOutliner:
    def __init__(self,imageData:ImageData,outlineColor:tuple):
        self.imageData = imageData
        self.outlineColor = outlineColor
    
    def shouldBeColored(self,pixelPos):
        if not self.imageData.isTransparent(pixelPos):
            return False

        # a pixel should be colored if any of its surrounding pixels
        # is not transparent, but the pixel itself is
        for surroundingPixel in self.imageData.getSurroundingPixels(pixelPos):
            if not self.imageData.isTransparent(surroundingPixel):
                return True
        return False
    
    def outline(self):
        pixelsToColor = []
        for x in range(self.imageData.width):
            for y in range(self.imageData.height):
                currentPixelPos = (x,y)
                if self.shouldBeColored(currentPixelPos):
                    pixelsToColor.append(currentPixelPos)
        colorPixels(self.imageData.image,self.outlineColor, pixelsToColor)
        
def main():
    outlineColor:tuple

    if len(argv[2:6]) == 4:
        outlineColor = tuple([int(colorComponent) for colorComponent in argv[2:6]])
    else:
        if len(argv[2:6]) != 0:
            print("The given outline color is not valid, so the default color is used: " + str(DEFAULT_OUTLINE_COLOR))
        outlineColor = DEFAULT_OUTLINE_COLOR

    imageName = argv[1]
    image:Image = Image.open(f"{imageName}.png")
    outliner:ImageOutliner = ImageOutliner(ImageData(image),outlineColor)
    outliner.outline()
    image.save(f"{imageName}-outlined.png")

if __name__ == "__main__":
    main()



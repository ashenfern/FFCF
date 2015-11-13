
#Save  simple plot n image
values <- c(1.0,20,3.0,7.8,9.6,7.4,6.2,9.4)
png(filename="C:\\Users\\ashfernando\\Documents\\RFiles\\Images\\Test.png")
plot(values)
dev.off()

#saving an time series
values <- c(1.2, 2.3, 3.5,4.5,3.2)
tsValues <- ts(values, frequency = 4)
plot(tsValues)
png(filename="C:\\Users\\ashfernando\\Documents\\RFiles\\Images\\Test2.png")
plot(values)
dev.off()
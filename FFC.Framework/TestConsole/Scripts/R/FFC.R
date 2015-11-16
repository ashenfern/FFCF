# Taking sample data
data1<-"http://rci.rutgers.edu/~rwomack/UNRATE.csv"
data2<-"http://rci.rutgers.edu/~rwomack/CPIAUCSL.csv"

#load sample data
data<-read.csv(data1, row.names=1)
#data$VALUE

#Monthly Transactions
tsDataMonthly <- ts(data$VALUE, start=c(1948,1), frequency = 12)
#tsDataMonthly
plot(tsDataMonthly)


#Weekly Transactions
tsDataWeekly <- ts(data$VALUE, start=c(1948,1), frequency = 365)
plot(tsDataWeekly)
?ts

library("ggplot2")
library("forecast")
?msts



#library("ggplot2")
#qplot(week, orders, data = tsDataWeekly, colour = as.factor(year), geom = "line")

y <- msts(tsDataWeekly, seasonal.periods=c(365.25))
#fit <- ets(y)
fit <- tbats(y)
fc <- forecast(fit,f=10)
#fc
head(fc)
plot(fc)

?msts
?tbats

#####################################################################
require(fpp)
num <- c(1,2,3,4,5,6)

#general
f1 <- meanf(num, h=1)
f2 <- rwf(num ,  h=1)
#Moving time average need to be done 

#Algos
fit1<- ets(num)
fit2 <- HoltWinters(num)

fcast1 <- forecast(fit1, h = 2)
fcast2 <- forecast(fit2, h = 2)

fcast1
fcast2
?forecast
f3
#summary(f1)
#?meanf


#### Holt winters

require(forecast)
list <- runif(800,100, 200)

tsValue = ts(list, start=c(2014,1), frequency = 365 )
#auto.arima, arima
fit <- HoltWinters(tsValue)
fcast <- forecast(fit, h =100)
fcast
plot(fcast)


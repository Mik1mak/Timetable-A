export class TotalTime {
  public static minutesInDay(date: Date) {
    let output = date.getHours() * 60;
    output += date.getMinutes();

    return output;
  }
}
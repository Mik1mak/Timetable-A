export class TotalTime {
  public static minutesInDay(date: Date) {
    let output = date.getHours() * 60;
    output += date.getMinutes();

    return output;
  }

  public static readonly dayOfWeeksArray = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday'
  ];
}
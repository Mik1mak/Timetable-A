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

  public static createDateTimeIso(week: number, day: number, timeString: string): string {
    
    let startDate: Date = new Date(Date.UTC(1, 0, (week-1) * 7 + day, 0, 0, 0, 0));
    startDate.setUTCFullYear(1);

    return `${startDate.toISOString().split('T')[0]}T${timeString}:00.000`;
  }
}
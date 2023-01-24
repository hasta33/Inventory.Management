import {AfterViewInit, Component, ElementRef} from '@angular/core';

@Component({
  selector: 'app-notfound',
  templateUrl: './notfound.component.html',
  styleUrls: ['./notfound.component.scss']
})
export class NotfoundComponent implements AfterViewInit {

  constructor(private elementRef: ElementRef) { }


  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = 'black';
  }
}
